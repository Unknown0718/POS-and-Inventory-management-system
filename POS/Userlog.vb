Imports MySql.Data.MySqlClient
Public Class Userlog
    Public Shared loggedInUser As String
    'This function appears when the username and password is incorrect this text will appear
    Private Sub ClearUsernameAndPasswordTextBoxes()
        txtusername.Text = ""
        txtusername.ForeColor = Color.Gray
        txtusername.Text = "Enter Username"

        txtpassword.Text = ""
        txtpassword.ForeColor = Color.Gray
        txtpassword.Text = "Enter Password"
    End Sub
    Private Sub Userlog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Display a confirmation dialog when the user attempts to close the form
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' If the user clicks No, cancel the form closing event
        If result = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Userlog.loggedInUser = txtusername.Text
        If (txtusername.Text = "") Then
            MsgBox("Please enter your username") 'output if the username is empty
            txtusername.Focus()
            Exit Sub
        End If
        If (txtpassword.Text = "") Then
            MsgBox("Please enter your password") 'output if the password is empty
            txtpassword.Focus()
            Exit Sub
        End If

        Dim conn As New MySqlConnection("Data source=localhost;database=pos;username=root;password=") 'connection to the database 
        conn.Open()
        Dim cmd As New MySqlCommand("select * From user WHERE BINARY Username=@username AND BINARY Password=@password", conn) 'connection to the table inside the database
        cmd.Parameters.AddWithValue("username", txtusername.Text.Trim)
        cmd.Parameters.AddWithValue("password", txtpassword.Text.Trim)
        Dim reader As MySqlDataReader = cmd.ExecuteReader

        If reader.Read Then 'the second form or the user interface will appear
            Form2.Show()
            Me.Hide()
        Else
            MsgBox("Incorrect username or password") 'the output if there's no record in the database
            ClearUsernameAndPasswordTextBoxes() ' Clear both username and password textboxes
        End If

        conn.Close()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged 'this part shows the password input in the textbox 2
        ' Show/hide password based on CheckBox state
        UpdatePasswordVisibility()
    End Sub

    Private Sub UpdatePasswordVisibility()
        If CheckBox1.Checked Then
            txtpassword.UseSystemPasswordChar = False
        Else
            txtpassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check the CheckBox when the form loads
        CheckBox1.Checked = True

        ' Show/hide password based on CheckBox state
        UpdatePasswordVisibility()

        txtusername.ForeColor = Color.Gray
        txtusername.Text = "Enter Username"

        ' Set shadow text for password textbox
        SetShadowTextPassword(txtpassword, "Enter Password", Color.Gray)

        Label4.Parent = PictureBox2
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' Create an instance of the new form
        Dim AdminlogForm As New Adminlog()

        ' Reset textboxes in Adminlog form
        ResetTextboxes(AdminlogForm)

        ' Show the new form
        AdminlogForm.Show()

        ' Optionally, you can hide the current form if you want to switch between forms
        Me.Hide()
    End Sub

    Private Sub ResetTextboxes(form As Form)
        Dim adminlogForm As Adminlog = TryCast(form, Adminlog)
        If adminlogForm IsNot Nothing Then
            adminlogForm.txtusername.Text = ""
            adminlogForm.txtusername.ForeColor = Color.Gray
            adminlogForm.txtusername.Text = "Enter Username"

            adminlogForm.txtpassword.Text = ""
            adminlogForm.txtpassword.ForeColor = Color.Gray
            adminlogForm.txtpassword.Text = "Enter Password"
        End If
    End Sub

    Private Sub SetShadowTextName(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub
    Private Sub txtusername_GotFocus(sender As Object, e As EventArgs) Handles txtusername.GotFocus
        ' Clear the shadow text when textbox is focused
        If txtusername.Text = "Enter Username" Then
            txtusername.Text = ""
            txtusername.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub txtusername_LostFocus(sender As Object, e As EventArgs) Handles txtusername.LostFocus
        ' Restore the shadow text if textbox is empty
        If txtusername.Text.Trim() = "" Then
            SetShadowTextName(txtusername, "Enter Username", Color.Gray)
        End If
    End Sub
    Private Sub SetShadowTextPassword(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub
    Private Sub txtpassword_GotFocus(sender As Object, e As EventArgs) Handles txtpassword.GotFocus
        ' Clear the shadow text when textbox is focused
        If txtpassword.Text = "Enter Password" Then
            txtpassword.Text = ""
            txtpassword.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub txtpassword_LostFocus(sender As Object, e As EventArgs) Handles txtpassword.LostFocus
        ' Restore the shadow text if textbox is empty
        If txtpassword.Text.Trim() = "" Then
            SetShadowTextPassword(txtpassword, "Enter Password", Color.Gray)
        End If
    End Sub

End Class
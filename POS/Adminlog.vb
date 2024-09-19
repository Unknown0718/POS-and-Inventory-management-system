Imports MySql.Data.MySqlClient
Public Class Adminlog

    Private Sub Adminlog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Display a confirmation dialog when the user attempts to close the form
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' If the user clicks No, cancel the form closing event
        If result = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    ' Function to set shadow text for the username textbox
    Private Sub SetShadowTextUsername()
        txtusername.Text = "Enter UserID"
        txtusername.ForeColor = Color.Gray
    End Sub

    ' Function to clear shadow text for the username textbox
    Private Sub ClearShadowTextUsername()
        If txtusername.Text = "Enter UserID" Then
            txtusername.Text = ""
            txtusername.ForeColor = Color.Black
        End If
    End Sub

    ' Function to set shadow text for the password textbox
    Private Sub SetShadowTextPassword()
        txtpassword.Text = "Enter Password"
        txtpassword.ForeColor = Color.Gray
    End Sub

    ' Function to clear shadow text for the password textbox
    Private Sub ClearShadowTextPassword()
        If txtpassword.Text = "Enter Password" Then
            txtpassword.Text = ""
            txtpassword.ForeColor = Color.Black
        End If
    End Sub

    ' Function to clear both username and password textboxes
    Private Sub ClearUsernameAndPasswordTextBoxes()
        txtusername.Text = ""
        txtusername.ForeColor = Color.Gray
        txtusername.Text = "Enter UserID"

        txtpassword.Text = ""
        txtpassword.ForeColor = Color.Gray
        txtpassword.Text = "Enter Password"
    End Sub

    ' Button click event handler
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (txtusername.Text = "") Then
            MsgBox("Please enter your userID") 'output if the username is empty
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
        Dim cmd As New MySqlCommand("select * From admin WHERE BINARY UserID=@userID  AND BINARY Password=@password", conn) 'connection to the table inside the database
        cmd.Parameters.AddWithValue("userID", txtusername.Text.Trim)
        cmd.Parameters.AddWithValue("password", txtpassword.Text.Trim)
        Dim reader As MySqlDataReader = cmd.ExecuteReader

        If reader.Read Then 'the second form or the user interface will appear
            Dashboard.Show()
            Me.Hide()
        Else
            MsgBox("Incorrect username or password") 'the output if there's no record in the database
            ClearUsernameAndPasswordTextBoxes() ' Clear both username and password textboxes
        End If

        conn.Close()
    End Sub

    ' Load event handler
    Private Sub Adminlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Parent = PictureBox2
        SetShadowTextUsername() ' Set shadow text for username textbox
        SetShadowTextPassword() ' Set shadow text for password textbox
        CheckBox1.Checked = True ' Automatically check the CheckBox
    End Sub

    ' Link label click event handler
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim Adminlog As New Adminlog()
        Userlog.txtusername.Text = ""
        Userlog.txtpassword.Text = ""
        ResetTextboxes()
        ' Show the new form
        Userlog.Show()

        ' Optionally, you can hide the current form if you want to switch between forms
        Me.Hide()
    End Sub
    Private Sub ResetTextboxes()
        Userlog.txtusername.Text = ""
        Userlog.txtusername.ForeColor = Color.Gray
        Userlog.txtusername.Text = "Enter Username"

        Userlog.txtpassword.Text = ""
        Userlog.txtpassword.ForeColor = Color.Gray
        Userlog.txtpassword.Text = "Enter Password"
    End Sub

    ' Text changed event handler for username textbox
    Private Sub txtusername_TextChanged(sender As Object, e As EventArgs) Handles txtusername.TextChanged
        If txtusername.Text <> "Enter UserID" Then
            txtusername.ForeColor = Color.Black
        End If
    End Sub

    ' Text changed event handler for password textbox
    Private Sub txtpassword_TextChanged(sender As Object, e As EventArgs) Handles txtpassword.TextChanged
        If txtpassword.Text <> "Enter Password" Then
            txtpassword.ForeColor = Color.Black
        End If
    End Sub

    ' Got focus event handler for username textbox
    Private Sub txtusername_GotFocus(sender As Object, e As EventArgs) Handles txtusername.GotFocus
        ClearShadowTextUsername()
    End Sub

    ' Lost focus event handler for username textbox
    Private Sub txtusername_LostFocus(sender As Object, e As EventArgs) Handles txtusername.LostFocus
        If txtusername.Text.Trim() = "" Then
            SetShadowTextUsername()
        End If
    End Sub

    ' Got focus event handler for password textbox
    Private Sub txtpassword_GotFocus(sender As Object, e As EventArgs) Handles txtpassword.GotFocus
        ClearShadowTextPassword()
    End Sub

    ' Lost focus event handler for password textbox
    Private Sub txtpassword_LostFocus(sender As Object, e As EventArgs) Handles txtpassword.LostFocus
        If txtpassword.Text.Trim() = "" Then
            SetShadowTextPassword()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged 'this part shows the password input in the textbox 2
        If CheckBox1.Checked Then
            txtpassword.UseSystemPasswordChar = False
        Else
            txtpassword.UseSystemPasswordChar = True

        End If
    End Sub
End Class


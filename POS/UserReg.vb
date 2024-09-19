Imports MySql.Data.MySqlClient
Imports System.IO
Public Class UserReg

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles Txtfirstname.GotFocus
        If Txtfirstname.Text = "First Name" Then
            Txtfirstname.Text = ""
            Txtfirstname.ForeColor = Color.Black ' Change text color when user starts typing
        End If
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles Txtfirstname.LostFocus
        If Txtfirstname.Text = "" Then
            Txtfirstname.Text = "First Name"
            Txtfirstname.ForeColor = Color.Gray ' Change text color back to placeholder color
        End If
    End Sub


    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TxtSecondName.GotFocus
        If TxtSecondName.Text = "Last Name" Then
            TxtSecondName.Text = ""
            TxtSecondName.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As EventArgs) Handles TxtSecondName.LostFocus
        If TxtSecondName.Text = "" Then
            TxtSecondName.Text = "Last Name"
            TxtSecondName.ForeColor = Color.Gray
        End If
    End Sub


    Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        If txtUsername.Text = "Username" Then
            txtUsername.Text = ""
            txtUsername.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox3_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        If txtUsername.Text = "" Then
            txtUsername.Text = "Username"
            txtUsername.ForeColor = Color.Gray
        End If
    End Sub


    Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        If txtPassword.Text = "Password" Then
            txtPassword.Text = ""
            txtPassword.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox4_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        If txtPassword.Text = "" Then
            txtPassword.Text = "Password"
            txtPassword.ForeColor = Color.Gray
        End If
    End Sub




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname2.Text = Adminlog.txtusername.Text ' This line of code shows the username of the user
        Txtfirstname.Text = "First Name"
        Txtfirstname.ForeColor = Color.Gray ' Set the initial text color to placeholder color

        TxtSecondName.Text = "Last Name"
        TxtSecondName.ForeColor = Color.Gray

        txtUsername.Text = "Username"
        txtUsername.ForeColor = Color.Gray

        txtPassword.Text = "Password"
        txtPassword.ForeColor = Color.Gray

    End Sub


    Dim connectionString As String = "Server=localhost;Database=pos;User=root;Password=;"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        Dim firstname As String = Txtfirstname.Text
        Dim lastname As String = TxtSecondName.Text
        Dim dateOfBirth As Date = dtpDateOfBirth.Value
        Dim gender As String = ""

        If rbMale.Checked Then
            gender = "Male"
        ElseIf rbFemale.Checked Then
            gender = "Female"

        End If

        ' Check if any of the required fields are empty
        If String.IsNullOrWhiteSpace(username) Or
            String.IsNullOrWhiteSpace(password) Or
            String.IsNullOrWhiteSpace(firstname) Or
            String.IsNullOrWhiteSpace(lastname) Or
            String.IsNullOrWhiteSpace(dateOfBirth) Or
            String.IsNullOrWhiteSpace(gender) Then

            MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit the method if validation fails
        End If

        Try
            connection.Open()

            Dim query As String = "INSERT INTO user (Username, Password, FirstName, SecondName, Gender, date_of_birth, user_photos) VALUES (@Username, @Password, @FirstName, @SecondName, @Gender, @DateOfBirth, @UserPhoto)"
            Dim command As MySqlCommand = New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@Username", username)
            command.Parameters.AddWithValue("@Password", password)
            command.Parameters.AddWithValue("@FirstName", firstname)
            command.Parameters.AddWithValue("@SecondName", lastname)
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth)
            command.Parameters.AddWithValue("@Gender", gender)

            ' Convert image to byte array
            Dim photoBytes As Byte() = File.ReadAllBytes(selectedImageFilePath)
            command.Parameters.AddWithValue("@UserPhoto", photoBytes)

            command.ExecuteNonQuery()
            MessageBox.Show("User registered successfully!")

            ' it will transfer you to another page when the registration is successful
            Dim Dashboard As New Dashboard()
            Dashboard.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: Not Registered")

        Finally
            connection.Close()
        End Try

    End Sub

    Dim selectedImageFilePath As String = "" ' Declare selectedImageFilePath at the class level
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            selectedImageFilePath = openFileDialog1.FileName
            PictureBox3.Image = Image.FromFile(selectedImageFilePath)
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
        End If
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Display a warning message
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)

        ' If the user confirms, proceed with logging out
        If result = DialogResult.OK Then
            Dim Adminlog As New Adminlog()

            Adminlog.txtusername.Text = ""
            Adminlog.txtpassword.Text = ""

            Adminlog.Show()

            Me.Close()
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim ManageSales As New ManageSales()
        ManageSales.Show()
        Me.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim Manage As New Manage()
        Manage.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Records As New Records()
        Records.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Dashboard As New Dashboard()
        Dashboard.Show()
        Me.Close()
    End Sub
End Class
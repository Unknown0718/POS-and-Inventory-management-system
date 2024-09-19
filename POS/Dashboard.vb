Public Class Dashboard

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim UserReg As New UserReg()
        UserReg.Show()
        Me.Close()
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

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim Manage As New Manage()
        Manage.Show()
        Me.Close()
    End Sub

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname2.Text = Adminlog.txtusername.Text ' This line of code shows the username of the user
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim ManageSales As New ManageSales()
        ManageSales.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Records As New Records()
        Records.Show()
        Me.Close()
    End Sub
End Class
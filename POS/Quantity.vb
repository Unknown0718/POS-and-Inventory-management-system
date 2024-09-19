Public Class Quantity
    Public Property Quantity As Integer
    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles ReduceButton.Click
        If Integer.TryParse(TextBox1.Text, Quantity) Then
            Me.DialogResult = DialogResult.OK
        Else
            MessageBox.Show("Invalid quantity. Please enter a valid number.")
        End If
    End Sub

    Private Sub ClearTextBoxInForm2()
        If Application.OpenForms("Form2") IsNot Nothing Then
            Dim form2 As Form2 = DirectCast(Application.OpenForms("Form2"), Form2)
            form2.TextBox1.Text = ""
        End If
    End Sub
    Private Sub Quantity_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ClearTextBoxInForm2()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Check if the pressed key is not a digit and not a control key
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' Cancel the keypress event
            e.Handled = True

        End If
    End Sub
End Class
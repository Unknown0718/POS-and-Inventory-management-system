Public Class QuantityReduction
    ' Property to set the maximum allowed reduction
    Public Property MaxQuantity As Integer

    ' Property to get the reduction amount
    Public ReadOnly Property ReductionAmount As Integer
        Get
            Return CInt(TextBox1.Text)
        End Get
    End Property

    Private Sub ReduceButton_Click(sender As Object, e As EventArgs) Handles ReduceButton.Click
        Dim reduction As Integer
        If Integer.TryParse(TextBox1.Text, reduction) Then
            ' Check if the reduction amount is within the allowed range
            If reduction > 0 AndAlso reduction <= MaxQuantity Then
                DialogResult = DialogResult.OK
                Close()
            Else
                MessageBox.Show("Please enter a valid reduction amount.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Please enter a valid integer value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Check if the pressed key is not a digit and not a control key
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' Cancel the keypress event
            e.Handled = True

        End If
    End Sub
End Class
Public Class Payment
    ' Declare a method to calculate the total price
    Public Function CalculateTotalPrice() As Decimal
        ' Add your logic here to calculate the total price
        ' For example, you could sum the prices of items in a DataGridView
        ' and return the total price
        Return 0 ' Placeholder return value
    End Function

    Public Sub New(ByVal totalPrice As Decimal)
        InitializeComponent()
        Label2.Text = totalPrice.ToString("C") ' Update Label2 with the total price
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Calculate change
        Dim totalPrice As Decimal = Decimal.Parse(Label2.Text, Globalization.NumberStyles.Currency)
        Dim paymentAmount As Decimal = Decimal.Parse(TextBox1.Text, Globalization.NumberStyles.Currency)
        Dim change As Decimal = paymentAmount - totalPrice

        ' Display change
        Label3.Text = change.ToString("C")
    End Sub
End Class


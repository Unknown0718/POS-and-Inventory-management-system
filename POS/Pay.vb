Public Class Pay
    ' Declare a variable to store the total price
    Private totalPrice As Decimal
    Private WithEvents DataGridView1 As New DataGridView()

    Public Sub New(ByVal totalValue As String)
        InitializeComponent()

        ' Set the text of Label2 to display the total price passed from Form1
        Label2.Text = totalValue

        ' Parse the total price from string to decimal
        If Decimal.TryParse(totalValue.Replace("Total Price: ₱", ""), totalPrice) = False Then
            totalPrice = 0
        End If
    End Sub


    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Check if the pressed key is not a digit and not a control key
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' Cancel the keypress event
            e.Handled = True

        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Calculate the remaining amount when text in TextBox1 changes
        Dim payment As Decimal
        If Decimal.TryParse(TextBox1.Text, payment) Then
            ' Calculate the remaining amount
            Dim remainingAmount As Decimal = payment - totalPrice

            ' Display the remaining amount in Label3 with the text "Change: "
            Label3.Text = "" & remainingAmount.ToString("0.00")
        Else
            ' If input is not a valid number, display an error or a message
            Label3.Text = "0.00"
        End If
    End Sub
    'This is the condition for the button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim paymentAmount As Decimal

        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show("Please input a payment amount.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(TextBox1.Text, paymentAmount) Then
            MessageBox.Show("Invalid payment amount. Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If paymentAmount < totalPrice Then
            MessageBox.Show("Payment amount is less than the total price. Additional payment is required.", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            ' Get the value of the payment from the TextBox
            'This shows the payment input
            Dim paymentValue As String = TextBox1.Text

            ' Pass paymentValue, the text for Label3, and the data source to the Receipt form
            Dim receiptForm As New Receipt(Form2, paymentValue, Form2.Label3.Text, Form2.DataGridView1.DataSource, Form2.lblname3.Text, Form2.Label4.Text)
            'This shows the change
            receiptForm.SetLabel4Text(Label3.Text)
            receiptForm.Show()
            Me.Close()

        End If
    End Sub

    Private Sub Pay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the form to be always on top
        Me.TopMost = True

        ' Check if the "Form2" form exists
        Dim form2 As Form = Application.OpenForms("Form2")
        If form2 IsNot Nothing Then
            ' Disable controls on the form behind
            For Each ctl As Control In form2.Controls
                ctl.Enabled = False
            Next
        End If
    End Sub

    Private Sub Pay_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Reset the TopMost property when the Pay form is closed
        Me.TopMost = False

        ' Check if the "Form2" form exists
        Dim form2 As Form = Application.OpenForms("Form2")
        If form2 IsNot Nothing Then
            ' Re-enable controls on the form behind
            For Each ctl As Control In form2.Controls
                ctl.Enabled = True
            Next

            ' Set focus to Form2
            form2.Focus()
        End If
    End Sub


End Class
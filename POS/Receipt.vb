Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Receipt
    Private mainForm As Form2 ' Field to store reference to Form2
    Private payForm As Pay ' Field to store reference to Pay form
    Private lblName3Text As String

    'This shows the change or the connection of label3 and label4
    Public Sub SetLabel4Text(text As String)
        Label4.Text = text
    End Sub


    Public Sub New(payForm As Pay, labelText As String, dataSource As Object)
        InitializeComponent()
        Me.payForm = payForm ' Store reference to the Pay form
        Label3.Text = labelText
        DataGridView1.DataSource = dataSource ' Set the data source for DataGridView1
    End Sub

    Public Sub New(mainForm As Form2, paymentValue As String, labelText As String, dataSource As DataTable, lblName3Text As String, label4Text As String)
        InitializeComponent()
        Me.mainForm = mainForm
        Label2.Text = paymentValue
        DataGridView1.DataSource = dataSource
        Label3.Text = labelText
        Label7.Text = "Name: " & lblName3Text
        Label10.Text = "Cashier No." & label4Text
    End Sub
    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the form to be always on top
        Me.TopMost = True

        ' Check if the "Form2" form exists
        Dim form2 As Form = Application.OpenForms("Form2")
        If form2 IsNot Nothing Then
            ' Disable the entire form
            form2.Enabled = False
        End If
        DataGridView1.RowHeadersVisible = False ' Hide row headers
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None ' Remove cell borders
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ' Resize columns to fill the width
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells ' Resize rows to fit content
        DataGridView1.ReadOnly = True  ' Make the DataGridView non-editable
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect ' Enable full row selection

        ' Set Label4 text to match Label3 text from the Pay form
        If payForm IsNot Nothing Then
            Label4.Text = payForm.Label3.Text
        End If
    End Sub

    Private Sub Receipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ' Reset the TopMost property when the Receipt form is closed
        Me.TopMost = False

        ' Check if the "Form2" form exists
        Dim form2 As Form = Application.OpenForms("Form2")
        If form2 IsNot Nothing Then
            ' Re-enable the entire form
            form2.Enabled = True

            ' Set focus to Form2
            form2.Focus()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Define your connection string
            Dim connectionString As String = "server=localhost;database=history;uid=root;password=;"

            ' SQL query to insert data into the database
            Dim query As String = "INSERT INTO stockout (Total, Payment, Chang, Cashname, Cashno) VALUES (@Total, @Payment, @Chang, @Cashname, @Cashno)"


            ' Create a connection object
            Using connection As New MySqlConnection(connectionString)
                ' Open the connection
                connection.Open()

                ' Create a command object with the query and connection
                Using command As New MySqlCommand(query, connection)
                    ' Set parameter values
                    command.Parameters.AddWithValue("@Total", Label3.Text) ' Total Price
                    command.Parameters.AddWithValue("@Payment", Label2.Text) ' Payment
                    command.Parameters.AddWithValue("@Chang", Label4.Text) 'Change
                    command.Parameters.AddWithValue("@Cashname", Label7.Text) ' Cashier Name
                    command.Parameters.AddWithValue("@Cashno", Label10.Text) ' Cashier Number

                    ' Execute the query
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Clearing the data source of Form2's DataGridView
            If mainForm IsNot Nothing AndAlso mainForm.DataGridView1 IsNot Nothing Then
                mainForm.DataGridView1.DataSource = Nothing
                mainForm.Label3.Text = "Total Price: ₱0.00"
            End If

            ' Close the Receipt form
            Me.Close()
        Catch ex As Exception
            ' Handle any exceptions
            MessageBox.Show("An error occurred while saving data to the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
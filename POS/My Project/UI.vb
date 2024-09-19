Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form2
    Dim conn As New MySqlConnection("Data source=localhost;database=inventory;username=root;password=")
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Check if the pressed key is not a digit and not a control key
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            ' Cancel the keypress event
            e.Handled = True

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text &= "1"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text &= "2"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text &= "3"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Text &= "4"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text &= "5"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text &= "6"
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox1.Text &= "7"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBox1.Text &= "8"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox1.Text &= "9"
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TextBox1.Text &= "0"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = "" ' Clear the text in the textbox
    End Sub




    Private Sub UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Show the username of the user
        lblname3.Text = Userlog.txtusername.Text

        ' Fetch and display the user's image in PictureBox1
        LoadUserImage()

        ' Additional UI setup
        DataGridView1.RowHeadersVisible = False ' Hide row headers
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None ' Remove cell borders
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ' Resize columns to fill the width
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells ' Resize rows to fit content

        ' Start the timer and update the date time
        Timer1.Start()
        UpdateDateTime()
    End Sub

    Private Sub LoadUserImage()
        Dim connectionString As String = "Data source=localhost;database=pos;username=root;password="

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT user_photos FROM User WHERE Username = @Username"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@Username", Userlog.loggedInUser) ' Use loggedInUser property
                Using reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Dim imageData As Byte() = DirectCast(reader("user_photos"), Byte())
                        If imageData IsNot Nothing Then
                            Using stream As New System.IO.MemoryStream(imageData)
                                PictureBox1.Image = Image.FromStream(stream)
                            End Using
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub UpdateDateTime()
        ' Update the label with the current date and time
        Label2.Text = DateTime.Now.ToString("MMMM dd, yyyy (dddd)")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = DateTime.Now.ToString("hh:mm:ss tt").ToUpper()
    End Sub
    Private Sub AddItemByIDButtonClick(sender As Object, e As EventArgs) Handles Button11.Click
        ' Get the item ID from the TextBox
        Dim itemId As Integer

        ' Check if the input is a valid integer
        If Not Integer.TryParse(TextBox1.Text, itemId) Then
            MessageBox.Show("Please enter a valid item ID.")
            Return
        End If

        ' Open the Quantity form to input the quantity
        Dim quantityForm As New Quantity()

        ' Show the quantity form as a dialog
        If quantityForm.ShowDialog() = DialogResult.OK Then
            ' If OK is clicked on the quantity form, get the quantity
            Dim quantity As Integer = quantityForm.Quantity

            ' Call the function to add item to grid with the quantity
            AddItemToGrid(itemId, quantity)

            ' Clear the TextBox
            TextBox1.Text = ""
        End If
    End Sub
    Private Sub UpdateTotalQuantityAndPrice()
        Dim totalQuantity As Integer = 0
        Dim totalPrice As Decimal = 0

        For Each row As DataGridViewRow In DataGridView1.Rows
            totalQuantity += Convert.ToInt32(row.Cells("Quantity").Value)
            totalPrice += Convert.ToDecimal(row.Cells("Total Price").Value)
        Next

        Label4.Text = "Total Quantity: " & totalQuantity.ToString()
        Label3.Text = "Total Price: ₱" & totalPrice.ToString("0.00")
    End Sub


    Private Sub AddItemToGrid(itemId As Integer, quantity As Integer)
        ' Fetch item details from the database using the item ID
        Try
            Dim commandText As String = "SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE ID = @id"
            Dim command As New MySqlCommand(commandText, conn)
            command.Parameters.AddWithValue("@id", itemId)

            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()

            conn.Open()
            adapter.Fill(table)

            If table.Rows.Count > 0 Then
                ' Item found, extract its details
                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                ' Calculate the total quantity of this item already in the grid
                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

                ' Check if adding the new quantity exceeds the available stock
                If totalQuantityInGrid + quantity > availableStock Then
                    Dim availableQuantityToAdd As Integer = availableStock - totalQuantityInGrid
                    If availableQuantityToAdd > 0 Then
                        MessageBox.Show("Only " & availableQuantityToAdd & " items can be added before reaching the maximum limit.")
                    Else
                        MessageBox.Show("Maximum stock reached. No items can be added.")
                    End If
                    Return
                End If


                Dim totalPrice As Decimal = quantity * price

                ' Check if the item is already in the DataGridView
                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        ' Update the quantity and total price of the existing row
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                ' If the item is not found, add a new row
                If Not found Then
                    ' Initialize DataGridView1.DataSource if it's null
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    ' Add the item to DataGridView
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                End If
            Else
                MessageBox.Show("Item not found.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
        UpdateTotalQuantityAndPrice()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim Snacks As New Snacks()
        Snacks.Show()
        Me.Hide()
    End Sub

    'Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
    '    Dim quantityForm As New Quantity()

    '    If quantityForm.ShowDialog() = DialogResult.OK Then
    '        Dim quantity As Integer = quantityForm.Quantity
    '        AddItemToGrid(quantity)
    '    End If
    'End Sub

    'Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
    '    Dim quantityForm As New Quantity()

    '    If quantityForm.ShowDialog() = DialogResult.OK Then
    '        Dim quantity As Integer = quantityForm.Quantity
    '        AddCloverToGrid(quantity)
    '    End If
    'End Sub

    'Private Sub AddItemToGrid(quantity As Integer)
    '    Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'piattos'", conn)
    '    Dim adapter As New MySqlDataAdapter(command)
    '    Dim table As New DataTable()

    '    Try
    '        conn.Open()
    '        adapter.Fill(table)

    '        If table.Rows.Count > 0 Then
    '            Dim itemName As String = table.Rows(0)("Name").ToString()
    '            Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
    '            Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

    '            If quantity > availableStock Then
    '                MessageBox.Show("Insufficient stock. Available stock: " & availableStock)
    '                Return
    '            End If

    '            Dim totalPrice As Decimal = quantity * price

    '            ' Check if DataSource is initialized
    '            If DataGridView1.DataSource Is Nothing Then
    '                DataGridView1.DataSource = New DataTable()
    '                DataGridView1.DataSource.Columns.Add("Name")
    '                DataGridView1.DataSource.Columns.Add("Quantity")
    '                DataGridView1.DataSource.Columns.Add("Price")
    '                DataGridView1.DataSource.Columns.Add("Total Price")
    '            End If

    '            ' Check if the item is already in the DataGridView
    '            Dim found As Boolean = False
    '            For Each row As DataGridViewRow In DataGridView1.Rows
    '                If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
    '                    row.Cells("Quantity").Value = Convert.ToInt32(row.Cells("Quantity").Value) + quantity
    '                    row.Cells("Total Price").Value = Convert.ToDecimal(row.Cells("Price").Value) * Convert.ToInt32(row.Cells("Quantity").Value)
    '                    found = True
    '                    Exit For
    '                End If
    '            Next

    '            ' If the item is not found, add a new row
    '            If Not found Then
    '                Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
    '                newRow("Name") = itemName
    '                newRow("Quantity") = quantity
    '                newRow("Price") = price
    '                newRow("Total Price") = totalPrice

    '                DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
    '            End If
    '        Else
    '            MessageBox.Show("No rows found with the name 'piattos'.")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        conn.Close()
    '    End Try
    '    UpdateTotalQuantityAndPrice()
    'End Sub

    'Private Sub AddCloverToGrid(quantity As Integer)
    '    Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Clover'", conn)
    '    Dim adapter As New MySqlDataAdapter(command)
    '    Dim table As New DataTable()

    '    Try
    '        conn.Open()
    '        adapter.Fill(table)

    '        If table.Rows.Count > 0 Then
    '            Dim itemName As String = table.Rows(0)("Name").ToString()
    '            Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
    '            Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

    '            If quantity > availableStock Then
    '                MessageBox.Show("Insufficient stock. Available stock: " & availableStock)
    '                Return
    '            End If

    '            Dim totalPrice As Decimal = quantity * price

    '            ' Initialize DataGridView1.DataSource if it's null
    '            If DataGridView1.DataSource Is Nothing Then
    '                Dim newTable As New DataTable()
    '                newTable.Columns.Add("Name")
    '                newTable.Columns.Add("Quantity")
    '                newTable.Columns.Add("Price")
    '                newTable.Columns.Add("Total Price")
    '                DataGridView1.DataSource = newTable
    '            End If

    '            ' Check if the item is already in the DataGridView
    '            Dim found As Boolean = False
    '            For Each row As DataGridViewRow In DataGridView1.Rows
    '                If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
    '                    row.Cells("Quantity").Value = Convert.ToInt32(row.Cells("Quantity").Value) + quantity
    '                    row.Cells("Total Price").Value = Convert.ToDecimal(row.Cells("Price").Value) * Convert.ToInt32(row.Cells("Quantity").Value)
    '                    found = True
    '                    Exit For
    '                End If
    '            Next

    '            ' If the item is not found, add a new row
    '            If Not found Then
    '                Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
    '                newRow("Name") = itemName
    '                newRow("Quantity") = quantity
    '                newRow("Price") = price
    '                newRow("Total Price") = totalPrice

    '                DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
    '            End If
    '        Else
    '            MessageBox.Show("No rows found with the name 'Clover'.")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        conn.Close()
    '    End Try
    '    UpdateTotalQuantityAndPrice()
    'End Sub

End Class
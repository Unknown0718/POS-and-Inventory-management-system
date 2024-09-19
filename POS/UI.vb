Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

'In the check out button when i click it 
'a form will show showing the total price And the Payment And also the change. you need To input your payment before decreasing the item In the database and this is done/

'And also in theadmin page i need to create a database for the checkout history 



'The - sign in the change in the receipt should be removed
'And for the categories i will add a function in the last button Or i will add an arrow button where when it gets clicked it will transfer to the next category
'fix the function issue of the input payment where the form should be the only one that can be clicked and the issue is that the form cashier can be clicked while the paymenty form shows

Public Class Form2


    Dim conn As New MySqlConnection("Data source=localhost;database=inventory;username=root;password=")

    Private button13Clicked As Boolean = False
    Private TotalPrice As Decimal = 0

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        If ListBox1.Visible Then
            ' ListBox1 is currently visible, hide it
            ListBox1.Visible = False

            ' If TextBox2 is visible, send it to the back and clear its text
            If TextBox2.Visible Then
                TextBox2.SendToBack()
                TextBox2.Clear()
            End If
        Else
            ' ListBox1 is currently not visible, show it
            ListBox1.Visible = True
            ListBox1.BringToFront()

            ' Define connection string
            Dim connStr As String = "Data Source=localhost;Database=inventory;Username=root;Password="

            ' Create connection
            Using conn As New MySqlConnection(connStr)
                Try
                    ' Open the connection
                    conn.Open()

                    ' Define SQL query
                    Dim query As String = "SELECT id, name FROM items"

                    ' Create command
                    Using cmd As New MySqlCommand(query, conn)
                        ' Execute command and read data
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            ' Clear existing items in the list box
                            ListBox1.Items.Clear()

                            ' Loop through the data and add to list box
                            While reader.Read()
                                ' Concatenate id and name and add to list box
                                Dim itemString As String = reader("id").ToString() & " - " & reader("name").ToString()
                                ListBox1.Items.Add(itemString)
                            End While
                        End Using
                    End Using
                Catch ex As Exception
                    ' Handle any errors
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using

            ' If TextBox2 is visible, bring it to the front
            If TextBox2.Visible Then
                TextBox2.BringToFront()
                ' Set pre-text if TextBox2 is empty
                If TextBox2.Text = "" Then
                    TextBox2.Text = "Search"
                    TextBox2.ForeColor = Color.Gray ' Set the font color to gray
                End If

                ' Set focus to TextBox2
                TextBox2.Focus()
            End If
        End If
    End Sub
    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        ' Check if the text is the shadow text
        If TextBox2.Text = "Search" Then
            ' Clear the shadow text
            TextBox2.Text = ""
            ' Change the font color to black
            TextBox2.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        ' Check if the text is empty
        If TextBox2.Text = "" Then
            ' Set the shadow text
            TextBox2.Text = "Search"
            ' Change the font color to gray
            TextBox2.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ' Clear the ListBox1 items
        ListBox1.Items.Clear()

        ' Retrieve the search term from TextBox2
        Dim searchTerm As String = TextBox2.Text

        ' Define connection string
        Dim connStr As String = "Data Source=localhost;Database=inventory;Username=root;Password="

        ' Create connection
        Using conn As New MySqlConnection(connStr)
            Try
                ' Open the connection
                conn.Open()

                ' Define SQL query
                Dim query As String = "SELECT id, name FROM items WHERE id LIKE @searchTerm OR name LIKE @searchTerm"

                ' Create command
                Using cmd As New MySqlCommand(query, conn)
                    ' Set the parameter value
                    cmd.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

                    ' Execute command and read data
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Loop through the data and add to list box
                        While reader.Read()
                            ' Concatenate id and name and add to list box
                            Dim itemString As String = reader("id").ToString() & " - " & reader("name").ToString()
                            ListBox1.Items.Add(itemString)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle any errors
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub
    Private isLoggingOut As Boolean = False

    Private Sub Logout_Click(sender As Object, e As EventArgs) Handles Logout.Click
        ' Display a confirmation message box
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' Check if the user confirmed the logout
        If result = DialogResult.Yes Then
            isLoggingOut = True ' Set the flag to indicate logout action
            Dim Userlog As New Userlog()

            Userlog.txtusername.Text = ""
            Userlog.txtpassword.Text = ""

            Userlog.Show()

            Me.Close()
        End If
    End Sub


    Private Sub UpdateLinkLabelText(itemName As String)
        Label5.Text = itemName
    End Sub

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

    Private Function GetUserByIdFromDatabase(username As String) As String
        Dim userId As String = ""

        ' SQL query to retrieve user ID based on the username
        Dim query As String = "SELECT ID FROM pos.user WHERE username = @Username"

        Try
            ' Open the connection
            conn.Open()

            ' Create command
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Username", username)

                ' Execute command and read data
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    ' Check if any rows were returned
                    If reader.Read() Then
                        ' Retrieve the user ID from the database
                        userId = reader("ID").ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("Error retrieving user ID: " & ex.Message)
        Finally
            ' Close the connection
            conn.Close()
        End Try

        Return userId
    End Function
    Private Sub UI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Check if the form is closing due to logout
        If Not isLoggingOut Then
            Dim dialog As DialogResult
            ' Display a confirmation dialog when the user attempts to close the form
            dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' If the user clicks No, cancel the form closing event
            If dialog = DialogResult.No Then
                e.Cancel = True
            Else
                Application.ExitThread()
            End If
        End If
    End Sub

    Private Sub UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Show the username of the user
        lblname3.Text = Userlog.txtusername.Text
        ' Retrieve the username from Userlog.txtusername.Text
        Dim username As String = Userlog.txtusername.Text

        ' Retrieve user ID from the database
        Dim userId As String = GetUserByIdFromDatabase(username)

        ' Display the retrieved user ID in Label4
        Label4.Text = userId
        ' Fetch and display the user's image in PictureBox1
        LoadUserImage()

        DataGridView1.RowHeadersVisible = False ' Hide row headers
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None ' Remove cell borders
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ' Resize columns to fill the width
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells ' Resize rows to fit content
        DataGridView1.ReadOnly = True  ' Make the DataGridView non-editable
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect ' Enable full row selection

        ' Start the timer and update the date time
        Timer1.Start()
        UpdateDateTime()
    End Sub
    'This code is for retrieving the photo of the user to show the image of the user from the database
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
        Dim totalPrice As Decimal = 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            totalPrice += Convert.ToDecimal(row.Cells("Total Price").Value)
        Next
        Label3.Text = "" & totalPrice.ToString("0.00")
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

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

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
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddPiattosToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Piattos")
        Label5.Text = itemName
    End Sub

    Private Sub Button51_MouseEnter(sender As Object, e As EventArgs) Handles Button51.MouseEnter
        UpdateLinkLabelText("Piattos")
    End Sub

    Private Sub Button51_MouseLeave(sender As Object, e As EventArgs) Handles Button51.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddPiattosToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Piattos'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

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
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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



    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCloverToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Clover")
        Label5.Text = itemName
    End Sub

    Private Sub Button29_MouseEnter(sender As Object, e As EventArgs) Handles Button29.MouseEnter
        UpdateLinkLabelText("Clover")
    End Sub
    Private Sub Button29_MouseLeave(sender As Object, e As EventArgs) Handles Button29.MouseLeave
        UpdateLinkLabelText("")
    End Sub


    Private Sub AddCloverToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Clover'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

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
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddChippyToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Chippy")
        Label5.Text = itemName
    End Sub

    Private Sub Button32_MouseEnter(sender As Object, e As EventArgs) Handles Button32.MouseEnter
        UpdateLinkLabelText("Chippy")
    End Sub
    Private Sub Button32_MouseLeave(sender As Object, e As EventArgs) Handles Button32.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddChippyToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Chippy'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then
                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddNachoToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Nacho")
        Label5.Text = itemName
    End Sub

    Private Sub Button34_MouseEnter(sender As Object, e As EventArgs) Handles Button34.MouseEnter
        UpdateLinkLabelText("Nacho")
    End Sub
    Private Sub Button34_MouseLeave(sender As Object, e As EventArgs) Handles Button34.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddNachoToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Nacho'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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


    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCornBitsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Nacho")
        Label5.Text = itemName
    End Sub

    Private Sub Button33_MouseEnter(sender As Object, e As EventArgs) Handles Button33.MouseEnter
        UpdateLinkLabelText("Corn Bits")
    End Sub
    Private Sub Button33_MouseLeave(sender As Object, e As EventArgs) Handles Button33.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddCornBitsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Corn Bits'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then
                ' Item found, extract its details
                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddRinbeeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Nacho")
        Label5.Text = itemName
    End Sub

    Private Sub Button41_MouseEnter(sender As Object, e As EventArgs) Handles Button41.MouseEnter
        UpdateLinkLabelText("Rinbee")
    End Sub
    Private Sub Button41_MouseLeave(sender As Object, e As EventArgs) Handles Button41.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddRinbeeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rinbee'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then
                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddRollerCoasterToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Roller Coaster")
        Label5.Text = itemName
    End Sub

    Private Sub Button39_MouseEnter(sender As Object, e As EventArgs) Handles Button39.MouseEnter
        UpdateLinkLabelText("Roller Coaster")
    End Sub
    Private Sub Button39_MouseLeave(sender As Object, e As EventArgs) Handles Button39.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddRollerCoasterToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Roller Coaster'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then
                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next

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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If

                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddPatataToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Patata")
        Label5.Text = itemName
    End Sub

    Private Sub Button42_MouseEnter(sender As Object, e As EventArgs) Handles Button42.MouseEnter
        UpdateLinkLabelText("Patata")
    End Sub
    Private Sub Button42_MouseLeave(sender As Object, e As EventArgs) Handles Button42.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddPatataToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Patata'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMrChipsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Mr.Chips")
        Label5.Text = itemName
    End Sub

    Private Sub Button43_MouseEnter(sender As Object, e As EventArgs) Handles Button43.MouseEnter
        UpdateLinkLabelText("Mr.Chips")
    End Sub
    Private Sub Button43_MouseLeave(sender As Object, e As EventArgs) Handles Button43.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMrChipsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Mr.Chips'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddVcutToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("V cut")
        Label5.Text = itemName
    End Sub

    Private Sub Button38_MouseEnter(sender As Object, e As EventArgs) Handles Button38.MouseEnter
        UpdateLinkLabelText("V cut")
    End Sub
    Private Sub Button38_MouseLeave(sender As Object, e As EventArgs) Handles Button38.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddVcutToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'V cut'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMagicFlakesToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Magic Flakes")
        Label5.Text = itemName
    End Sub

    Private Sub Button35_MouseEnter(sender As Object, e As EventArgs) Handles Button35.MouseEnter
        UpdateLinkLabelText("Magic Flakes")
    End Sub
    Private Sub Button35_MouseLeave(sender As Object, e As EventArgs) Handles Button35.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMagicFlakesToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Magic Flakes'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMilkyKnotsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Magic Flakes")
        Label5.Text = itemName
    End Sub

    Private Sub Button40_MouseEnter(sender As Object, e As EventArgs) Handles Button40.MouseEnter
        UpdateLinkLabelText("Magic Flakes")
    End Sub
    Private Sub Button40_MouseLeave(sender As Object, e As EventArgs) Handles Button40.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMilkyKnotsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Magic Flakes'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCheezeCurlsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cheeze Curls")
        Label5.Text = itemName
    End Sub

    Private Sub Button36_MouseEnter(sender As Object, e As EventArgs) Handles Button36.MouseEnter
        UpdateLinkLabelText("Cheeze Curls")
    End Sub
    Private Sub Button36_MouseLeave(sender As Object, e As EventArgs) Handles Button36.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddCheezeCurlsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cheeze Curls'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddBreadPanToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("BreadPan")
        Label5.Text = itemName
    End Sub

    Private Sub Button45_MouseEnter(sender As Object, e As EventArgs) Handles Button45.MouseEnter
        UpdateLinkLabelText("BreadPan")
    End Sub
    Private Sub Button45_MouseLeave(sender As Object, e As EventArgs) Handles Button45.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddBreadPanToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'BreadPan'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddPotatoChipsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Potato Chips")
        Label5.Text = itemName
    End Sub

    Private Sub Button37_MouseEnter(sender As Object, e As EventArgs) Handles Button37.MouseEnter
        UpdateLinkLabelText("Potato Chips")
    End Sub
    Private Sub Button37_MouseLeave(sender As Object, e As EventArgs) Handles Button37.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddPotatoChipsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Potato Chips'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddPotatoFriesToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Potato Fries")
        Label5.Text = itemName
    End Sub

    Private Sub Button44_MouseEnter(sender As Object, e As EventArgs) Handles Button44.MouseEnter
        UpdateLinkLabelText("Potato Fries")
    End Sub
    Private Sub Button44_MouseLeave(sender As Object, e As EventArgs) Handles Button44.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddPotatoFriesToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Potato Fries'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    'This is the buttons for the ice cream
    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddHersheysToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Hersheys Ice Cream (Selecta)")
        Label5.Text = itemName
    End Sub
    Private Sub Button50_MouseEnter(sender As Object, e As EventArgs) Handles Button50.MouseEnter
        UpdateLinkLabelText("Hersheys Ice Cream (Selecta)")
    End Sub
    Private Sub Button50_MouseLeave(sender As Object, e As EventArgs) Handles Button50.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddHersheysToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Hersheys Ice Cream (Selecta)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddSelectaCookiesToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cookies And Cream Ice Cream (Selecta)")
        Label5.Text = itemName
    End Sub
    Private Sub Button66_MouseEnter(sender As Object, e As EventArgs) Handles Button66.MouseEnter
        UpdateLinkLabelText("Cookies And Cream Ice Cream (Selecta)")
    End Sub
    Private Sub Button66_MouseLeave(sender As Object, e As EventArgs) Handles Button66.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddSelectaCookiesToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cookies And Cream Ice Cream (Selecta)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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


    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddStrawberryToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Strawberry Ice Cream (Selecta)")
        Label5.Text = itemName
    End Sub
    Private Sub Button30_MouseEnter(sender As Object, e As EventArgs) Handles Button30.MouseEnter
        UpdateLinkLabelText("Strawberry Ice Cream (Selecta)")
    End Sub
    Private Sub Button30_MouseLeave(sender As Object, e As EventArgs) Handles Button30.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddStrawberryToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Strawberry Ice Cream (Selecta)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddDarkChocolateToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Dark Chocolate Ice Cream (Selecta)")
        Label5.Text = itemName
    End Sub
    Private Sub Button52_MouseEnter(sender As Object, e As EventArgs) Handles Button52.MouseEnter
        UpdateLinkLabelText("Dark Chocolate Ice Cream (Selecta)")
    End Sub
    Private Sub Button52_MouseLeave(sender As Object, e As EventArgs) Handles Button52.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddDarkChocolateToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Dark Chocolate Ice Cream (Selecta)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCreambarToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cookies And Cream Ice Cream (Creamline)")
        Label5.Text = itemName
    End Sub
    Private Sub Button61_MouseEnter(sender As Object, e As EventArgs) Handles Button61.MouseEnter
        UpdateLinkLabelText("Cookies And Cream Ice Cream (Creamline)")
    End Sub
    Private Sub Button61_MouseLeave(sender As Object, e As EventArgs) Handles Button61.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddCreambarToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cookies And Cream Ice Cream (Creamline)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button62_Click(sender As Object, e As EventArgs) Handles Button62.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMochiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Mochi Ice Cream")
        Label5.Text = itemName
    End Sub
    Private Sub Button62_MouseEnter(sender As Object, e As EventArgs) Handles Button62.MouseEnter
        UpdateLinkLabelText("Mochi Icre Cream")
    End Sub
    Private Sub Button62_MouseLeave(sender As Object, e As EventArgs) Handles Button62.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMochiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Mochi Ice Cream'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button63_Click(sender As Object, e As EventArgs) Handles Button63.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCoconutToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Coconut Ice Cream (Creamline)")
        Label5.Text = itemName
    End Sub
    Private Sub Button63_MouseEnter(sender As Object, e As EventArgs) Handles Button63.MouseEnter
        UpdateLinkLabelText("Coconut Ice Cream (Creamline)")
    End Sub
    Private Sub Button63_MouseLeave(sender As Object, e As EventArgs) Handles Button63.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddCoconutToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Coconut Ice Cream (Creamline)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddPinipigToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pinipig Ice Cream (Creamline)")
        Label5.Text = itemName
    End Sub
    Private Sub Button64_MouseEnter(sender As Object, e As EventArgs) Handles Button64.MouseEnter
        UpdateLinkLabelText("Pinipig Ice Cream (Creamline)")
    End Sub
    Private Sub Button64_MouseLeave(sender As Object, e As EventArgs) Handles Button64.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddPinipigToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pinipig Ice Cream (Creamline)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddRedVelvetToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Red Velvet (Magnum)")
        Label5.Text = itemName
    End Sub
    Private Sub Button58_MouseEnter(sender As Object, e As EventArgs) Handles Button58.MouseEnter
        UpdateLinkLabelText("Red Velvet (Magnum)")
    End Sub
    Private Sub Button58_MouseLeave(sender As Object, e As EventArgs) Handles Button58.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddRedVelvetToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Red Velvet (Magnum)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddWhiteChocolateToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("White Chocolate (Magnum)")
        Label5.Text = itemName
    End Sub
    Private Sub Button59_MouseEnter(sender As Object, e As EventArgs) Handles Button59.MouseEnter
        UpdateLinkLabelText("White Chocolate (Magnum)")
    End Sub
    Private Sub Button59_MouseLeave(sender As Object, e As EventArgs) Handles Button59.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddWhiteChocolateToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'White Chocolate (Magnum)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddAlmondToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Almond Chocolate (Magnum)")
        Label5.Text = itemName
    End Sub
    Private Sub Button60_MouseEnter(sender As Object, e As EventArgs) Handles Button60.MouseEnter
        UpdateLinkLabelText("Almond Chocolate (Magnum)")
    End Sub
    Private Sub Button60_MouseLeave(sender As Object, e As EventArgs) Handles Button60.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddAlmondToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Almond Chocolate (Magnum)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddRemixToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("(Magnum) Remix")
        Label5.Text = itemName
    End Sub
    Private Sub Button57_MouseEnter(sender As Object, e As EventArgs) Handles Button57.MouseEnter
        UpdateLinkLabelText("(Magnum) Remix")
    End Sub
    Private Sub Button57_MouseLeave(sender As Object, e As EventArgs) Handles Button57.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddRemixToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = '(Magnum) Remix'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddVanillaToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Vanilla Ice Cream (Nestle)")
        Label5.Text = itemName
    End Sub
    Private Sub Button49_MouseEnter(sender As Object, e As EventArgs) Handles Button49.MouseEnter
        UpdateLinkLabelText("Vanilla Ice Cream (Nestle)")
    End Sub
    Private Sub Button49_MouseLeave(sender As Object, e As EventArgs) Handles Button49.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddVanillaToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Vanilla Ice Cream (Nestle)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddKitkatToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Kitkat Chocolate Ice Cream (Nestle)")
        Label5.Text = itemName
    End Sub
    Private Sub Button46_MouseEnter(sender As Object, e As EventArgs) Handles Button46.MouseEnter
        UpdateLinkLabelText("Kitkat Chocolate Ice Cream (Nestle)")
    End Sub
    Private Sub Button46_MouseLeave(sender As Object, e As EventArgs) Handles Button46.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddKitkatToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Kitkat Chocolate Ice Cream (Nestle)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddKitkattToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Kitkat White Chocolate Ice Cream (Nestle)")
        Label5.Text = itemName
    End Sub
    Private Sub Button47_MouseEnter(sender As Object, e As EventArgs) Handles Button47.MouseEnter
        UpdateLinkLabelText("Kitkat White Chocolate Ice Cream (Nestle)")
    End Sub
    Private Sub Button47_MouseLeave(sender As Object, e As EventArgs) Handles Button47.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddKitkattToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Kitkat White Chocolate Ice Cream (Nestle)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddChocolateToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Chocolate Ice Cream (Nestle)")
        Label5.Text = itemName
    End Sub
    Private Sub Button48_MouseEnter(sender As Object, e As EventArgs) Handles Button48.MouseEnter
        UpdateLinkLabelText("Chocolate Ice Cream (Nestle)")
    End Sub
    Private Sub Button48_MouseLeave(sender As Object, e As EventArgs) Handles Button48.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddChocolateToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Chocolate Ice Cream (Nestle)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddCokeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Coke")
        Label5.Text = itemName
    End Sub
    Private Sub Button56_MouseEnter(sender As Object, e As EventArgs) Handles Button56.MouseEnter
        UpdateLinkLabelText("Coke")
    End Sub
    Private Sub Button56_MouseLeave(sender As Object, e As EventArgs) Handles Button56.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddCokeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Coke'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddsprToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Sprite")
        Label5.Text = itemName
    End Sub
    Private Sub Button67_MouseEnter(sender As Object, e As EventArgs) Handles Button67.MouseEnter
        UpdateLinkLabelText("Sprite")
    End Sub
    Private Sub Button67_MouseLeave(sender As Object, e As EventArgs) Handles Button67.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddsprToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Sprite'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button68_Click(sender As Object, e As EventArgs) Handles Button68.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddroyalToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Royal")
        Label5.Text = itemName
    End Sub
    Private Sub Button68_MouseEnter(sender As Object, e As EventArgs) Handles Button68.MouseEnter
        UpdateLinkLabelText("Royal")
    End Sub
    Private Sub Button68_MouseLeave(sender As Object, e As EventArgs) Handles Button68.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddroyalToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Royal'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button69_Click(sender As Object, e As EventArgs) Handles Button69.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrcToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("RC Cola")
        Label5.Text = itemName
    End Sub
    Private Sub Button69_MouseEnter(sender As Object, e As EventArgs) Handles Button69.MouseEnter
        UpdateLinkLabelText("RC Cola")
    End Sub
    Private Sub Button69_MouseLeave(sender As Object, e As EventArgs) Handles Button69.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrcToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'RC Cola'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button71_Click(sender As Object, e As EventArgs) Handles Button71.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmountToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Mountain Dew")
        Label5.Text = itemName
    End Sub
    Private Sub Button71_MouseEnter(sender As Object, e As EventArgs) Handles Button71.MouseEnter
        UpdateLinkLabelText("Mountain Dew")
    End Sub
    Private Sub Button71_MouseLeave(sender As Object, e As EventArgs) Handles Button71.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmountToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Mountain Dew'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button70_Click(sender As Object, e As EventArgs) Handles Button70.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpepToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pepsi")
        Label5.Text = itemName
    End Sub
    Private Sub Button70_MouseEnter(sender As Object, e As EventArgs) Handles Button70.MouseEnter
        UpdateLinkLabelText("Pepsi")
    End Sub
    Private Sub Button70_MouseLeave(sender As Object, e As EventArgs) Handles Button70.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpepToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pepsi'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button72_Click(sender As Object, e As EventArgs) Handles Button72.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddupToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("7 up")
        Label5.Text = itemName
    End Sub
    Private Sub Button72_MouseEnter(sender As Object, e As EventArgs) Handles Button72.MouseEnter
        UpdateLinkLabelText("7 up")
    End Sub
    Private Sub Button72_MouseLeave(sender As Object, e As EventArgs) Handles Button72.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddupToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = '7 up'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button73_Click(sender As Object, e As EventArgs) Handles Button73.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            Addc2ToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("C2")
        Label5.Text = itemName
    End Sub
    Private Sub Button73_MouseEnter(sender As Object, e As EventArgs) Handles Button73.MouseEnter
        UpdateLinkLabelText("C2")
    End Sub
    Private Sub Button73_MouseLeave(sender As Object, e As EventArgs) Handles Button73.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Addc2ToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'C2'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button75_Click(sender As Object, e As EventArgs) Handles Button75.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddzestToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Zesto Mango")
        Label5.Text = itemName
    End Sub
    Private Sub Button75_MouseEnter(sender As Object, e As EventArgs) Handles Button75.MouseEnter
        UpdateLinkLabelText("Zesto Mango")
    End Sub
    Private Sub Button75_MouseLeave(sender As Object, e As EventArgs) Handles Button75.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddzestToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Zesto Mango'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button74_Click(sender As Object, e As EventArgs) Handles Button74.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddChocoToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Zesto Choco")
        Label5.Text = itemName
    End Sub
    Private Sub Button74_MouseEnter(sender As Object, e As EventArgs) Handles Button74.MouseEnter
        UpdateLinkLabelText("Zesto Choco")
    End Sub
    Private Sub Button74_MouseLeave(sender As Object, e As EventArgs) Handles Button74.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddChocoToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Zesto Choco'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button76_Click(sender As Object, e As EventArgs) Handles Button76.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMinToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Minute Maid")
        Label5.Text = itemName
    End Sub
    Private Sub Button76_MouseEnter(sender As Object, e As EventArgs) Handles Button76.MouseEnter
        UpdateLinkLabelText("Minute Maid")
    End Sub
    Private Sub Button76_MouseLeave(sender As Object, e As EventArgs) Handles Button76.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMinToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Minute Maid'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddjungToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Jungle Juice")
        Label5.Text = itemName
    End Sub
    Private Sub Button77_MouseEnter(sender As Object, e As EventArgs) Handles Button77.MouseEnter
        UpdateLinkLabelText("Jungle Juice")
    End Sub
    Private Sub Button77_MouseLeave(sender As Object, e As EventArgs) Handles Button77.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddjungToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Jungle Juice'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button79_Click(sender As Object, e As EventArgs) Handles Button79.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddMoguToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Mogu Mogu")
        Label5.Text = itemName
    End Sub
    Private Sub Button79_MouseEnter(sender As Object, e As EventArgs) Handles Button79.MouseEnter
        UpdateLinkLabelText("Mogu Mogu")
    End Sub
    Private Sub Button79_MouseLeave(sender As Object, e As EventArgs) Handles Button79.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddMoguToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Mogu Mogu'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button78_Click(sender As Object, e As EventArgs) Handles Button78.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddDelightToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Delight")
        Label5.Text = itemName
    End Sub
    Private Sub Button78_MouseEnter(sender As Object, e As EventArgs) Handles Button78.MouseEnter
        UpdateLinkLabelText("Delight")
    End Sub
    Private Sub Button78_MouseLeave(sender As Object, e As EventArgs) Handles Button78.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddDelightToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Delight'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button80_Click(sender As Object, e As EventArgs) Handles Button80.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddYakToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Yakult")
        Label5.Text = itemName
    End Sub
    Private Sub Button80_MouseEnter(sender As Object, e As EventArgs) Handles Button80.MouseEnter
        UpdateLinkLabelText("Yakult")
    End Sub
    Private Sub Button80_MouseLeave(sender As Object, e As EventArgs) Handles Button80.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddYakToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Yakult'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button81_Click(sender As Object, e As EventArgs) Handles Button81.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddnatToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Nata De Coco")
        Label5.Text = itemName
    End Sub
    Private Sub Button81_MouseEnter(sender As Object, e As EventArgs) Handles Button81.MouseEnter
        UpdateLinkLabelText("Nata De Coco")
    End Sub
    Private Sub Button81_MouseLeave(sender As Object, e As EventArgs) Handles Button81.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddnatToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Nata De Coco'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button113_Click(sender As Object, e As EventArgs) Handles Button113.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcheeseToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cheese Sticks")
        Label5.Text = itemName
    End Sub
    Private Sub Button113_MouseEnter(sender As Object, e As EventArgs) Handles Button113.MouseEnter
        UpdateLinkLabelText("Cheese Sticks")
    End Sub
    Private Sub Button113_MouseLeave(sender As Object, e As EventArgs) Handles Button113.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcheeseToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cheese Sticks'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button129_Click(sender As Object, e As EventArgs) Handles Button129.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmangToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("O Puff Mango")
        Label5.Text = itemName
    End Sub
    Private Sub Button129_MouseEnter(sender As Object, e As EventArgs) Handles Button129.MouseEnter
        UpdateLinkLabelText("O Puff Mango")
    End Sub
    Private Sub Button129_MouseLeave(sender As Object, e As EventArgs) Handles Button129.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmangToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'O Puff Mango'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button145_Click(sender As Object, e As EventArgs) Handles Button145.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddstrawToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Crackers (Strawberry)")
        Label5.Text = itemName
    End Sub
    Private Sub Button145_MouseEnter(sender As Object, e As EventArgs) Handles Button145.MouseEnter
        UpdateLinkLabelText("Rebisco Crackers (Strawberry)")
    End Sub
    Private Sub Button145_MouseLeave(sender As Object, e As EventArgs) Handles Button145.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddstrawToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Crackers (Strawberry)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button161_Click(sender As Object, e As EventArgs) Handles Button161.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddSkinToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Skin Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button161_MouseEnter(sender As Object, e As EventArgs) Handles Button161.MouseEnter
        UpdateLinkLabelText("Skin Silog")
    End Sub
    Private Sub Button161_MouseLeave(sender As Object, e As EventArgs) Handles Button161.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddSkinToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Skin Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button162_Click(sender As Object, e As EventArgs) Handles Button162.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddgreenToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Green Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button162_MouseEnter(sender As Object, e As EventArgs) Handles Button162.MouseEnter
        UpdateLinkLabelText("Green Expanded Folder")
    End Sub
    Private Sub Button162_MouseLeave(sender As Object, e As EventArgs) Handles Button162.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddgreenToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Green Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button82_Click(sender As Object, e As EventArgs) Handles Button82.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddchickToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Chicken)")
        Label5.Text = itemName
    End Sub
    Private Sub Button82_MouseEnter(sender As Object, e As EventArgs) Handles Button82.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Chicken)")
    End Sub
    Private Sub Button82_MouseLeave(sender As Object, e As EventArgs) Handles Button82.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddchickToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Chicken)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button83_Click(sender As Object, e As EventArgs) Handles Button83.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddspicupToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Spicy Seafood)")
        Label5.Text = itemName
    End Sub
    Private Sub Button83_MouseEnter(sender As Object, e As EventArgs) Handles Button83.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Spicy Seafood)")
    End Sub
    Private Sub Button83_MouseLeave(sender As Object, e As EventArgs) Handles Button83.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddspicupToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Spicy Seafood)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button112_Click(sender As Object, e As EventArgs) Handles Button112.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddfriesToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("French Fries")
        Label5.Text = itemName
    End Sub
    Private Sub Button112_MouseEnter(sender As Object, e As EventArgs) Handles Button112.MouseEnter
        UpdateLinkLabelText("French Fries")
    End Sub
    Private Sub Button112_MouseLeave(sender As Object, e As EventArgs) Handles Button112.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddfriesToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'French Fries'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button128_Click(sender As Object, e As EventArgs) Handles Button128.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddochocToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("O Puff Chocolate")
        Label5.Text = itemName
    End Sub
    Private Sub Button128_MouseEnter(sender As Object, e As EventArgs) Handles Button128.MouseEnter
        UpdateLinkLabelText("O Puff Chocolate")
    End Sub
    Private Sub Button128_MouseLeave(sender As Object, e As EventArgs) Handles Button128.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddochocToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'O Puff Chocolate'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button144_Click(sender As Object, e As EventArgs) Handles Button144.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrebchocToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Crackers (Chocolate)")
        Label5.Text = itemName
    End Sub
    Private Sub Button144_MouseEnter(sender As Object, e As EventArgs) Handles Button144.MouseEnter
        UpdateLinkLabelText("Rebisco Crackers (Chocolate)")
    End Sub
    Private Sub Button144_MouseLeave(sender As Object, e As EventArgs) Handles Button144.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrebchocToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Crackers (Chocolate)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button160_Click(sender As Object, e As EventArgs) Handles Button160.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddburgToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Burger Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button160_MouseEnter(sender As Object, e As EventArgs) Handles Button160.MouseEnter
        UpdateLinkLabelText("Burger Silog")
    End Sub
    Private Sub Button160_MouseLeave(sender As Object, e As EventArgs) Handles Button160.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddburgToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Burger Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button163_Click(sender As Object, e As EventArgs) Handles Button163.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddRedfolToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Red Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button163_MouseEnter(sender As Object, e As EventArgs) Handles Button163.MouseEnter
        UpdateLinkLabelText("Red Expanded Folder")
    End Sub
    Private Sub Button163_MouseLeave(sender As Object, e As EventArgs) Handles Button163.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddRedfolToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Red Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button84_Click(sender As Object, e As EventArgs) Handles Button84.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddseafToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Seafood)")
        Label5.Text = itemName
    End Sub
    Private Sub Button84_MouseEnter(sender As Object, e As EventArgs) Handles Button84.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Seafood)")
    End Sub
    Private Sub Button84_MouseLeave(sender As Object, e As EventArgs) Handles Button84.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddseafToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Seafood)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button111_Click(sender As Object, e As EventArgs) Handles Button111.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddsioToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Siomai")
        Label5.Text = itemName
    End Sub
    Private Sub Button111_MouseEnter(sender As Object, e As EventArgs) Handles Button111.MouseEnter
        UpdateLinkLabelText("Siomai")
    End Sub
    Private Sub Button111_MouseLeave(sender As Object, e As EventArgs) Handles Button111.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddsioToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Siomai'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button127_Click(sender As Object, e As EventArgs) Handles Button127.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddubeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("O Puff Ube")
        Label5.Text = itemName
    End Sub
    Private Sub Button127_MouseEnter(sender As Object, e As EventArgs) Handles Button127.MouseEnter
        UpdateLinkLabelText("O Puff Ube")
    End Sub
    Private Sub Button127_MouseLeave(sender As Object, e As EventArgs) Handles Button127.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddubeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'O Puff Ube'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button143_Click(sender As Object, e As EventArgs) Handles Button143.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrebpeaToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Crackers (Peanut Butter)")
        Label5.Text = itemName
    End Sub
    Private Sub Button143_MouseEnter(sender As Object, e As EventArgs) Handles Button143.MouseEnter
        UpdateLinkLabelText("Rebisco Crackers (Peanut Butter)")
    End Sub
    Private Sub Button143_MouseLeave(sender As Object, e As EventArgs) Handles Button143.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrebpeaToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Crackers (Peanut Butter)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button159_Click(sender As Object, e As EventArgs) Handles Button159.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddporkToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pork Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button159_MouseEnter(sender As Object, e As EventArgs) Handles Button159.MouseEnter
        UpdateLinkLabelText("Pork Silog")
    End Sub
    Private Sub Button159_MouseLeave(sender As Object, e As EventArgs) Handles Button159.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddporkToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pork Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button164_Click(sender As Object, e As EventArgs) Handles Button164.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddyellfoldToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Yellow Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button164_MouseEnter(sender As Object, e As EventArgs) Handles Button164.MouseEnter
        UpdateLinkLabelText("Yellow Expanded Folder")
    End Sub
    Private Sub Button164_MouseLeave(sender As Object, e As EventArgs) Handles Button164.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddyellfoldToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Yellow Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button85_Click(sender As Object, e As EventArgs) Handles Button85.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddseacheeseToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Cheesy Seafood)")
        Label5.Text = itemName
    End Sub
    Private Sub Button85_MouseEnter(sender As Object, e As EventArgs) Handles Button85.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Cheesy Seafood)")
    End Sub
    Private Sub Button85_MouseLeave(sender As Object, e As EventArgs) Handles Button85.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddseacheeseToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Cheesy Seafood'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button110_Click(sender As Object, e As EventArgs) Handles Button110.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddsoshiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Soshiwong")
        Label5.Text = itemName
    End Sub
    Private Sub Button110_MouseEnter(sender As Object, e As EventArgs) Handles Button110.MouseEnter
        UpdateLinkLabelText("Soshiwong")
    End Sub
    Private Sub Button110_MouseLeave(sender As Object, e As EventArgs) Handles Button110.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddsoshiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Soshiwong'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button126_Click(sender As Object, e As EventArgs) Handles Button126.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmatchToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("O Puff Matcha")
        Label5.Text = itemName
    End Sub
    Private Sub Button126_MouseEnter(sender As Object, e As EventArgs) Handles Button126.MouseEnter
        UpdateLinkLabelText("O Puff Matcha")
    End Sub
    Private Sub Button126_MouseLeave(sender As Object, e As EventArgs) Handles Button126.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmatchToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'O Puff Matcha'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button142_Click(sender As Object, e As EventArgs) Handles Button142.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbutterToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Crackers (Butter)")
        Label5.Text = itemName
    End Sub
    Private Sub Button142_MouseEnter(sender As Object, e As EventArgs) Handles Button142.MouseEnter
        UpdateLinkLabelText("Rebisco Crackers (Butter)")
    End Sub
    Private Sub Button142_MouseLeave(sender As Object, e As EventArgs) Handles Button142.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbutterToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Crackers (Butter)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button158_Click(sender As Object, e As EventArgs) Handles Button158.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddchicksilToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Chicken Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button158_MouseEnter(sender As Object, e As EventArgs) Handles Button158.MouseEnter
        UpdateLinkLabelText("Chicken Silog")
    End Sub
    Private Sub Button158_MouseLeave(sender As Object, e As EventArgs) Handles Button158.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddchicksilToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Chicken Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button165_Click(sender As Object, e As EventArgs) Handles Button165.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpurpToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Purple Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button165_MouseEnter(sender As Object, e As EventArgs) Handles Button165.MouseEnter
        UpdateLinkLabelText("Purple Expanded Folder")
    End Sub
    Private Sub Button165_MouseLeave(sender As Object, e As EventArgs) Handles Button165.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpurpToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Purple Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button86_Click(sender As Object, e As EventArgs) Handles Button86.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbatchoyToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Batchoy)")
        Label5.Text = itemName
    End Sub
    Private Sub Button86_MouseEnter(sender As Object, e As EventArgs) Handles Button86.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Batchoy)")
    End Sub
    Private Sub Button86_MouseLeave(sender As Object, e As EventArgs) Handles Button86.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbatchoyToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Batchoy)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button109_Click(sender As Object, e As EventArgs) Handles Button109.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcornToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Corndog")
        Label5.Text = itemName
    End Sub
    Private Sub Button109_MouseEnter(sender As Object, e As EventArgs) Handles Button109.MouseEnter
        UpdateLinkLabelText("Corndog")
    End Sub
    Private Sub Button109_MouseLeave(sender As Object, e As EventArgs) Handles Button109.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcornToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Corndog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button125_Click(sender As Object, e As EventArgs) Handles Button125.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddwhiteToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("White Rabbit")
        Label5.Text = itemName
    End Sub
    Private Sub Button125_MouseEnter(sender As Object, e As EventArgs) Handles Button125.MouseEnter
        UpdateLinkLabelText("White Rabbit")
    End Sub
    Private Sub Button125_MouseLeave(sender As Object, e As EventArgs) Handles Button125.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddwhiteToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'White Rabbit'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button141_Click(sender As Object, e As EventArgs) Handles Button141.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddskyToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Sky Flakes")
        Label5.Text = itemName
    End Sub
    Private Sub Button141_MouseEnter(sender As Object, e As EventArgs) Handles Button141.MouseEnter
        UpdateLinkLabelText("Sky Flakes")
    End Sub
    Private Sub Button141_MouseLeave(sender As Object, e As EventArgs) Handles Button141.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddskyToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Sky Flakes'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button157_Click(sender As Object, e As EventArgs) Handles Button157.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddhamToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Ham Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button157_MouseEnter(sender As Object, e As EventArgs) Handles Button157.MouseEnter
        UpdateLinkLabelText("Ham Silog")
    End Sub
    Private Sub Button157_MouseLeave(sender As Object, e As EventArgs) Handles Button157.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddhamToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Ham Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button166_Click(sender As Object, e As EventArgs) Handles Button166.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddorangeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Orange Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button166_MouseEnter(sender As Object, e As EventArgs) Handles Button166.MouseEnter
        UpdateLinkLabelText("Orange Expanded Folder")
    End Sub
    Private Sub Button166_MouseLeave(sender As Object, e As EventArgs) Handles Button166.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddorangeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Orange Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button87_Click(sender As Object, e As EventArgs) Handles Button87.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbeefToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Beef)")
        Label5.Text = itemName
    End Sub
    Private Sub Button87_MouseEnter(sender As Object, e As EventArgs) Handles Button87.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Beef)")
    End Sub
    Private Sub Button87_MouseLeave(sender As Object, e As EventArgs) Handles Button87.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbeefToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Beef)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button108_Click(sender As Object, e As EventArgs) Handles Button108.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddempToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Empanada")
        Label5.Text = itemName
    End Sub
    Private Sub Button108_MouseEnter(sender As Object, e As EventArgs) Handles Button108.MouseEnter
        UpdateLinkLabelText("Empanada")
    End Sub
    Private Sub Button108_MouseLeave(sender As Object, e As EventArgs) Handles Button108.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddempToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Empanada'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button124_Click(sender As Object, e As EventArgs) Handles Button124.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddguavpToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Guava")
        Label5.Text = itemName
    End Sub
    Private Sub Button124_MouseEnter(sender As Object, e As EventArgs) Handles Button124.MouseEnter
        UpdateLinkLabelText("Guava")
    End Sub
    Private Sub Button124_MouseLeave(sender As Object, e As EventArgs) Handles Button124.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddguavpToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Guava'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button140_Click(sender As Object, e As EventArgs) Handles Button140.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrebToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Honey Butter")
        Label5.Text = itemName
    End Sub
    Private Sub Button140_MouseEnter(sender As Object, e As EventArgs) Handles Button140.MouseEnter
        UpdateLinkLabelText("Rebisco Honey Butter")
    End Sub
    Private Sub Button140_MouseLeave(sender As Object, e As EventArgs) Handles Button140.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrebToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Honey Butter'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button156_Click(sender As Object, e As EventArgs) Handles Button156.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddtapToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Tapsilog")
        Label5.Text = itemName
    End Sub
    Private Sub Button156_MouseEnter(sender As Object, e As EventArgs) Handles Button156.MouseEnter
        UpdateLinkLabelText("Tapsilog")
    End Sub
    Private Sub Button156_MouseLeave(sender As Object, e As EventArgs) Handles Button156.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddtapToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Tapsilog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button88_Click(sender As Object, e As EventArgs) Handles Button88.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddBulToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Bulalo)")
        Label5.Text = itemName
    End Sub
    Private Sub Button88_MouseEnter(sender As Object, e As EventArgs) Handles Button88.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Bulalo)")
    End Sub
    Private Sub Button88_MouseLeave(sender As Object, e As EventArgs) Handles Button88.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddBulToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Bulalo)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button107_Click(sender As Object, e As EventArgs) Handles Button107.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddhotToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Hotdog Sandwich")
        Label5.Text = itemName
    End Sub
    Private Sub Button107_MouseEnter(sender As Object, e As EventArgs) Handles Button107.MouseEnter
        UpdateLinkLabelText("Hotdog Sandwich")
    End Sub
    Private Sub Button107_MouseLeave(sender As Object, e As EventArgs) Handles Button107.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddhotToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Hotdog Sandwich'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button123_Click(sender As Object, e As EventArgs) Handles Button123.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddjudgeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Judge")
        Label5.Text = itemName
    End Sub
    Private Sub Button123_MouseEnter(sender As Object, e As EventArgs) Handles Button123.MouseEnter
        UpdateLinkLabelText("Judge")
    End Sub
    Private Sub Button123_MouseLeave(sender As Object, e As EventArgs) Handles Button123.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddjudgeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Judge'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button167_Click(sender As Object, e As EventArgs) Handles Button167.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddblueToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Blue Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button167_MouseEnter(sender As Object, e As EventArgs) Handles Button167.MouseEnter
        UpdateLinkLabelText("Blue Expanded Folder")
    End Sub
    Private Sub Button167_MouseLeave(sender As Object, e As EventArgs) Handles Button167.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddblueToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Blue Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button139_Click(sender As Object, e As EventArgs) Handles Button139.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrebiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Rebisco Crackers")
        Label5.Text = itemName
    End Sub
    Private Sub Button139_MouseEnter(sender As Object, e As EventArgs) Handles Button139.MouseEnter
        UpdateLinkLabelText("Rebisco Crackers")
    End Sub
    Private Sub Button139_MouseLeave(sender As Object, e As EventArgs) Handles Button139.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrebiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Rebisco Crackers'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button155_Click(sender As Object, e As EventArgs) Handles Button155.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddlongToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Long Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button155_MouseEnter(sender As Object, e As EventArgs) Handles Button155.MouseEnter
        UpdateLinkLabelText("Long Silog")
    End Sub
    Private Sub Button155_MouseLeave(sender As Object, e As EventArgs) Handles Button155.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddlongToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Long Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button168_Click(sender As Object, e As EventArgs) Handles Button168.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmintToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Mint Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button168_MouseEnter(sender As Object, e As EventArgs) Handles Button168.MouseEnter
        UpdateLinkLabelText("Mint Expanded Folder")
    End Sub
    Private Sub Button168_MouseLeave(sender As Object, e As EventArgs) Handles Button168.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmintToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Mint Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button89_Click(sender As Object, e As EventArgs) Handles Button89.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcreamToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Cup Noodles (Creamy Seafood)")
        Label5.Text = itemName
    End Sub
    Private Sub Button89_MouseEnter(sender As Object, e As EventArgs) Handles Button89.MouseEnter
        UpdateLinkLabelText("Cup Noodles (Creamy Seafood)")
    End Sub
    Private Sub Button89_MouseLeave(sender As Object, e As EventArgs) Handles Button89.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcreamToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Cup Noodles (Creamy Seafood)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button106_Click(sender As Object, e As EventArgs) Handles Button106.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddtokToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Tokneneng")
        Label5.Text = itemName
    End Sub
    Private Sub Button106_MouseEnter(sender As Object, e As EventArgs) Handles Button106.MouseEnter
        UpdateLinkLabelText("Tokneneng")
    End Sub
    Private Sub Button106_MouseLeave(sender As Object, e As EventArgs) Handles Button106.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddtokToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Tokneneng'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button122_Click(sender As Object, e As EventArgs) Handles Button122.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddvToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("V Fresh")
        Label5.Text = itemName
    End Sub
    Private Sub Button122_MouseEnter(sender As Object, e As EventArgs) Handles Button122.MouseEnter
        UpdateLinkLabelText("V Fresh")
    End Sub
    Private Sub Button122_MouseLeave(sender As Object, e As EventArgs) Handles Button122.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddvToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'V Fresh'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button138_Click(sender As Object, e As EventArgs) Handles Button138.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddhoneyToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Honey House")
        Label5.Text = itemName
    End Sub
    Private Sub Button138_MouseEnter(sender As Object, e As EventArgs) Handles Button138.MouseEnter
        UpdateLinkLabelText("Honey House")
    End Sub
    Private Sub Button138_MouseLeave(sender As Object, e As EventArgs) Handles Button138.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddhoneyToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Honey House'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button154_Click(sender As Object, e As EventArgs) Handles Button154.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddhotsiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Hot Silog")
        Label5.Text = itemName
    End Sub
    Private Sub Button154_MouseEnter(sender As Object, e As EventArgs) Handles Button154.MouseEnter
        UpdateLinkLabelText("Hot Silog")
    End Sub
    Private Sub Button154_MouseLeave(sender As Object, e As EventArgs) Handles Button154.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddhotsiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Hot Silog'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button169_Click(sender As Object, e As EventArgs) Handles Button169.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbrToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Brown Expanded Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button169_MouseEnter(sender As Object, e As EventArgs) Handles Button169.MouseEnter
        UpdateLinkLabelText("Brown Expanded Folder")
    End Sub
    Private Sub Button169_MouseLeave(sender As Object, e As EventArgs) Handles Button169.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbrToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Brown Expanded Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button97_Click(sender As Object, e As EventArgs) Handles Button97.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpancToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pancit Canton")
        Label5.Text = itemName
    End Sub
    Private Sub Button97_MouseEnter(sender As Object, e As EventArgs) Handles Button97.MouseEnter
        UpdateLinkLabelText("Pancit Canton")
    End Sub
    Private Sub Button97_MouseLeave(sender As Object, e As EventArgs) Handles Button97.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpancToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancit Canton'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button105_Click(sender As Object, e As EventArgs) Handles Button105.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddkikToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Kikiam")
        Label5.Text = itemName
    End Sub
    Private Sub Button105_MouseEnter(sender As Object, e As EventArgs) Handles Button105.MouseEnter
        UpdateLinkLabelText("Kikiam")
    End Sub
    Private Sub Button105_MouseLeave(sender As Object, e As EventArgs) Handles Button105.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddkikToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Kikiam'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button121_Click(sender As Object, e As EventArgs) Handles Button121.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmaxToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Max Candy")
        Label5.Text = itemName
    End Sub
    Private Sub Button121_MouseEnter(sender As Object, e As EventArgs) Handles Button121.MouseEnter
        UpdateLinkLabelText("Max Candy")
    End Sub
    Private Sub Button121_MouseLeave(sender As Object, e As EventArgs) Handles Button121.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmaxToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Max Candy'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button137_Click(sender As Object, e As EventArgs) Handles Button137.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddorToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Oreo")
        Label5.Text = itemName
    End Sub
    Private Sub Button137_MouseEnter(sender As Object, e As EventArgs) Handles Button137.MouseEnter
        UpdateLinkLabelText("Oreo")
    End Sub
    Private Sub Button137_MouseLeave(sender As Object, e As EventArgs) Handles Button137.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddorToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Oreo'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button153_Click(sender As Object, e As EventArgs) Handles Button153.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddspagToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Spaghetti")
        Label5.Text = itemName
    End Sub
    Private Sub Button153_MouseEnter(sender As Object, e As EventArgs) Handles Button153.MouseEnter
        UpdateLinkLabelText("Spaghetti")
    End Sub
    Private Sub Button153_MouseLeave(sender As Object, e As EventArgs) Handles Button153.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddspagToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Spaghetti'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button177_Click(sender As Object, e As EventArgs) Handles Button177.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddblflToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Blue Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button177_MouseEnter(sender As Object, e As EventArgs) Handles Button177.MouseEnter
        UpdateLinkLabelText("Blue Long Folder")
    End Sub
    Private Sub Button177_MouseLeave(sender As Object, e As EventArgs) Handles Button177.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddblflToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Blue Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button96_Click(sender As Object, e As EventArgs) Handles Button96.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpccToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pansit Canton (Calamansi)")
        Label5.Text = itemName
    End Sub
    Private Sub Button96_MouseEnter(sender As Object, e As EventArgs) Handles Button96.MouseEnter
        UpdateLinkLabelText("Pancit Canton (Calamansi)")
    End Sub
    Private Sub Button96_MouseLeave(sender As Object, e As EventArgs) Handles Button96.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpccToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancit Canton (Calamansi)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button104_Click(sender As Object, e As EventArgs) Handles Button104.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcbToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Chicken Balls")
        Label5.Text = itemName
    End Sub
    Private Sub Button104_MouseEnter(sender As Object, e As EventArgs) Handles Button104.MouseEnter
        UpdateLinkLabelText("Chicken Balls")
    End Sub
    Private Sub Button104_MouseLeave(sender As Object, e As EventArgs) Handles Button104.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcbToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Chicken Balls'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button120_Click(sender As Object, e As EventArgs) Handles Button120.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddsbToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Snowbear")
        Label5.Text = itemName
    End Sub
    Private Sub Button120_MouseEnter(sender As Object, e As EventArgs) Handles Button120.MouseEnter
        UpdateLinkLabelText("Snowbear")
    End Sub
    Private Sub Button120_MouseLeave(sender As Object, e As EventArgs) Handles Button120.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddsbToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Snowbear'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button136_Click(sender As Object, e As EventArgs) Handles Button136.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddhiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Hi-Ro")
        Label5.Text = itemName
    End Sub
    Private Sub Button136_MouseEnter(sender As Object, e As EventArgs) Handles Button136.MouseEnter
        UpdateLinkLabelText("Hi-Ro")
    End Sub
    Private Sub Button136_MouseLeave(sender As Object, e As EventArgs) Handles Button136.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddhiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Hi-Ro'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button152_Click(sender As Object, e As EventArgs) Handles Button152.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcarToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Carbonara")
        Label5.Text = itemName
    End Sub
    Private Sub Button152_MouseEnter(sender As Object, e As EventArgs) Handles Button152.MouseEnter
        UpdateLinkLabelText("Carbonara")
    End Sub
    Private Sub Button152_MouseLeave(sender As Object, e As EventArgs) Handles Button152.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcarToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Carbonara'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button176_Click(sender As Object, e As EventArgs) Handles Button176.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddorflToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Orange Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button176_MouseEnter(sender As Object, e As EventArgs) Handles Button176.MouseEnter
        UpdateLinkLabelText("Orange Long Folder")
    End Sub
    Private Sub Button176_MouseLeave(sender As Object, e As EventArgs) Handles Button176.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddorflToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Orange Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button95_Click(sender As Object, e As EventArgs) Handles Button95.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpcToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pancit Canton (Chilimansi)")
        Label5.Text = itemName
    End Sub
    Private Sub Button95_MouseEnter(sender As Object, e As EventArgs) Handles Button95.MouseEnter
        UpdateLinkLabelText("Pansit Canton (Chilimansi)")
    End Sub
    Private Sub Button95_MouseLeave(sender As Object, e As EventArgs) Handles Button95.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpcToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancit Canton (Chilimansi)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button103_Click(sender As Object, e As EventArgs) Handles Button103.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddtrToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Turon")
        Label5.Text = itemName
    End Sub
    Private Sub Button103_MouseEnter(sender As Object, e As EventArgs) Handles Button103.MouseEnter
        UpdateLinkLabelText("Turon")
    End Sub
    Private Sub Button103_MouseLeave(sender As Object, e As EventArgs) Handles Button103.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddtrToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Turon'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button119_Click(sender As Object, e As EventArgs) Handles Button119.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpotToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Potchi")
        Label5.Text = itemName
    End Sub
    Private Sub Button119_MouseEnter(sender As Object, e As EventArgs) Handles Button119.MouseEnter
        UpdateLinkLabelText("Potchi")
    End Sub
    Private Sub Button119_MouseLeave(sender As Object, e As EventArgs) Handles Button119.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpotToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Potchi'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button135_Click(sender As Object, e As EventArgs) Handles Button135.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddprToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Presto")
        Label5.Text = itemName
    End Sub
    Private Sub Button135_MouseEnter(sender As Object, e As EventArgs) Handles Button135.MouseEnter
        UpdateLinkLabelText("Presto")
    End Sub
    Private Sub Button135_MouseLeave(sender As Object, e As EventArgs) Handles Button135.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddprToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Presto'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button151_Click(sender As Object, e As EventArgs) Handles Button151.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddchToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Champorado")
        Label5.Text = itemName
    End Sub
    Private Sub Button151_MouseEnter(sender As Object, e As EventArgs) Handles Button151.MouseEnter
        UpdateLinkLabelText("Champorado")
    End Sub
    Private Sub Button151_MouseLeave(sender As Object, e As EventArgs) Handles Button151.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddchToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Champorado'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button175_Click(sender As Object, e As EventArgs) Handles Button175.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddgrflToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Green Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button175_MouseEnter(sender As Object, e As EventArgs) Handles Button175.MouseEnter
        UpdateLinkLabelText("Green Long Folder")
    End Sub
    Private Sub Button175_MouseLeave(sender As Object, e As EventArgs) Handles Button175.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddgrflToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Green Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button94_Click(sender As Object, e As EventArgs) Handles Button94.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddxpcToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pancit Canton (Extra Hot)")
        Label5.Text = itemName
    End Sub
    Private Sub Button94_MouseEnter(sender As Object, e As EventArgs) Handles Button94.MouseEnter
        UpdateLinkLabelText("Pancit Canton (Extra Hot)")
    End Sub
    Private Sub Button94_MouseLeave(sender As Object, e As EventArgs) Handles Button94.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddxpcToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancit Canton (Extra Hot)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button102_Click(sender As Object, e As EventArgs) Handles Button102.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbcToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Banana Cue")
        Label5.Text = itemName
    End Sub
    Private Sub Button102_MouseEnter(sender As Object, e As EventArgs) Handles Button102.MouseEnter
        UpdateLinkLabelText("Banana Cue")
    End Sub
    Private Sub Button102_MouseLeave(sender As Object, e As EventArgs) Handles Button102.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbcToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Banana Cue'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button118_Click(sender As Object, e As EventArgs) Handles Button118.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddchampToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Champi")
        Label5.Text = itemName
    End Sub
    Private Sub Button118_MouseEnter(sender As Object, e As EventArgs) Handles Button118.MouseEnter
        UpdateLinkLabelText("Champi")
    End Sub
    Private Sub Button118_MouseLeave(sender As Object, e As EventArgs) Handles Button118.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddchampToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Champi'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button134_Click(sender As Object, e As EventArgs) Handles Button134.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddFitToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fita")
        Label5.Text = itemName
    End Sub
    Private Sub Button134_MouseEnter(sender As Object, e As EventArgs) Handles Button134.MouseEnter
        UpdateLinkLabelText("Fita")
    End Sub
    Private Sub Button134_MouseLeave(sender As Object, e As EventArgs) Handles Button134.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddFitToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fita'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button150_Click(sender As Object, e As EventArgs) Handles Button150.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpanToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pancake")
        Label5.Text = itemName
    End Sub
    Private Sub Button150_MouseEnter(sender As Object, e As EventArgs) Handles Button150.MouseEnter
        UpdateLinkLabelText("Pancake")
    End Sub
    Private Sub Button150_MouseLeave(sender As Object, e As EventArgs) Handles Button150.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpanToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancake'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button174_Click(sender As Object, e As EventArgs) Handles Button174.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrdflToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Red Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button174_MouseEnter(sender As Object, e As EventArgs) Handles Button174.MouseEnter
        UpdateLinkLabelText("Red Long Folder")
    End Sub
    Private Sub Button174_MouseLeave(sender As Object, e As EventArgs) Handles Button174.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrdflToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Red Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button93_Click(sender As Object, e As EventArgs) Handles Button93.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddspToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pansit Canton (Sweet And Spicy)")
        Label5.Text = itemName
    End Sub
    Private Sub Button93_MouseEnter(sender As Object, e As EventArgs) Handles Button93.MouseEnter
        UpdateLinkLabelText("Pancit Canton (Sweet And Spicy)")
    End Sub
    Private Sub Button93_MouseLeave(sender As Object, e As EventArgs) Handles Button93.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddspToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pancit Canton (Sweet And Spicy)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button117_Click(sender As Object, e As EventArgs) Handles Button117.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddfrsToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fruitos")
        Label5.Text = itemName
    End Sub
    Private Sub Button117_MouseEnter(sender As Object, e As EventArgs) Handles Button117.MouseEnter
        UpdateLinkLabelText("Fruitos")
    End Sub
    Private Sub Button117_MouseLeave(sender As Object, e As EventArgs) Handles Button117.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddfrsToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fruitos'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button133_Click(sender As Object, e As EventArgs) Handles Button133.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddfgToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fudgee Bar (Chocolate)")
        Label5.Text = itemName
    End Sub
    Private Sub Button133_MouseEnter(sender As Object, e As EventArgs) Handles Button133.MouseEnter
        UpdateLinkLabelText("Fudgee Bar (Chocolate)")
    End Sub
    Private Sub Button133_MouseLeave(sender As Object, e As EventArgs) Handles Button133.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddfgToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fudgee Bar (Chocolate)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button173_Click(sender As Object, e As EventArgs) Handles Button173.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpurplToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Violet Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button173_MouseEnter(sender As Object, e As EventArgs) Handles Button173.MouseEnter
        UpdateLinkLabelText("Violet Long Folder")
    End Sub
    Private Sub Button173_MouseLeave(sender As Object, e As EventArgs) Handles Button173.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpurplToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Violet Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button172_Click(sender As Object, e As EventArgs) Handles Button172.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpnkToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pink Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button172_MouseEnter(sender As Object, e As EventArgs) Handles Button172.MouseEnter
        UpdateLinkLabelText("Pink Long Folder")
    End Sub
    Private Sub Button172_MouseLeave(sender As Object, e As EventArgs) Handles Button172.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpnkToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pink Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button132_Click(sender As Object, e As EventArgs) Handles Button132.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddFudgeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fudgee Bar (Custard)")
        Label5.Text = itemName
    End Sub
    Private Sub Button132_MouseEnter(sender As Object, e As EventArgs) Handles Button132.MouseEnter
        UpdateLinkLabelText("Fudgee Bar (Custard)")
    End Sub
    Private Sub Button132_MouseLeave(sender As Object, e As EventArgs) Handles Button132.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddFudgeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fudgee Bar (Custard)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button116_Click(sender As Object, e As EventArgs) Handles Button116.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddyakiToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Yakee")
        Label5.Text = itemName
    End Sub
    Private Sub Button116_MouseEnter(sender As Object, e As EventArgs) Handles Button116.MouseEnter
        UpdateLinkLabelText("Yakee")
    End Sub
    Private Sub Button116_MouseLeave(sender As Object, e As EventArgs) Handles Button116.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddyakiToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Yakee'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button115_Click(sender As Object, e As EventArgs) Handles Button115.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddmonToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Monami")
        Label5.Text = itemName
    End Sub
    Private Sub Button115_MouseEnter(sender As Object, e As EventArgs) Handles Button115.MouseEnter
        UpdateLinkLabelText("Monami")
    End Sub
    Private Sub Button115_MouseLeave(sender As Object, e As EventArgs) Handles Button115.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddmonToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Monami'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button131_Click(sender As Object, e As EventArgs) Handles Button131.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddvanToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fudgee Bar (Vanilla)")
        Label5.Text = itemName
    End Sub
    Private Sub Button131_MouseEnter(sender As Object, e As EventArgs) Handles Button131.MouseEnter
        UpdateLinkLabelText("Fudgee Bar (Vanilla)")
    End Sub
    Private Sub Button131_MouseLeave(sender As Object, e As EventArgs) Handles Button131.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddvanToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fudgee Bar (Vanilla)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button171_Click(sender As Object, e As EventArgs) Handles Button171.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddbrowToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Brown Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button171_MouseEnter(sender As Object, e As EventArgs) Handles Button171.MouseEnter
        UpdateLinkLabelText("Brown Long Folder")
    End Sub
    Private Sub Button171_MouseLeave(sender As Object, e As EventArgs) Handles Button171.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddbrowToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Brown Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button114_Click(sender As Object, e As EventArgs) Handles Button114.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddchutToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Choc Nut")
        Label5.Text = itemName
    End Sub
    Private Sub Button114_MouseEnter(sender As Object, e As EventArgs) Handles Button114.MouseEnter
        UpdateLinkLabelText("Choc Nut")
    End Sub
    Private Sub Button114_MouseLeave(sender As Object, e As EventArgs) Handles Button114.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddchutToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Choc Nut'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button130_Click(sender As Object, e As EventArgs) Handles Button130.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AdddurToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Fudgee Bar (Durian")
        Label5.Text = itemName
    End Sub
    Private Sub Button130_MouseEnter(sender As Object, e As EventArgs) Handles Button130.MouseEnter
        UpdateLinkLabelText("Fudgee Bar (Durian)")
    End Sub
    Private Sub Button130_MouseLeave(sender As Object, e As EventArgs) Handles Button130.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AdddurToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Fudgee Bar (Durian)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button170_Click(sender As Object, e As EventArgs) Handles Button170.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddplToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Plain Long Folder")
        Label5.Text = itemName
    End Sub
    Private Sub Button170_MouseEnter(sender As Object, e As EventArgs) Handles Button170.MouseEnter
        UpdateLinkLabelText("Plain Long Folder")
    End Sub
    Private Sub Button170_MouseLeave(sender As Object, e As EventArgs) Handles Button170.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddplToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Plain Long Folder'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button90_Click(sender As Object, e As EventArgs) Handles Button90.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddpeToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Pencil Eraser")
        Label5.Text = itemName
    End Sub
    Private Sub Button90_MouseEnter(sender As Object, e As EventArgs) Handles Button90.MouseEnter
        UpdateLinkLabelText("Pencil Eraser")
    End Sub
    Private Sub Button90_MouseLeave(sender As Object, e As EventArgs) Handles Button90.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddpeToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Pencil Eraser'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button91_Click(sender As Object, e As EventArgs) Handles Button91.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddctToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Correction Tape")
        Label5.Text = itemName
    End Sub
    Private Sub Button91_MouseEnter(sender As Object, e As EventArgs) Handles Button91.MouseEnter
        UpdateLinkLabelText("Correction Tape")
    End Sub
    Private Sub Button91_MouseLeave(sender As Object, e As EventArgs) Handles Button91.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddctToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Correction Tape'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button92_Click(sender As Object, e As EventArgs) Handles Button92.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddclToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Correction Liquid")
        Label5.Text = itemName
    End Sub
    Private Sub Button92_MouseEnter(sender As Object, e As EventArgs) Handles Button92.MouseEnter
        UpdateLinkLabelText("Correction Liquid")
    End Sub
    Private Sub Button92_MouseLeave(sender As Object, e As EventArgs) Handles Button92.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddclToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Correction Liquid'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button99_Click(sender As Object, e As EventArgs) Handles Button99.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddyellToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Yellow Pad")
        Label5.Text = itemName
    End Sub
    Private Sub Button99_MouseEnter(sender As Object, e As EventArgs) Handles Button99.MouseEnter
        UpdateLinkLabelText("Yellow Pad")
    End Sub
    Private Sub Button99_MouseLeave(sender As Object, e As EventArgs) Handles Button99.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddyellToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Yellow Pad'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddinterToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Intermediate Pad")
        Label5.Text = itemName
    End Sub
    Private Sub Button100_MouseEnter(sender As Object, e As EventArgs) Handles Button100.MouseEnter
        UpdateLinkLabelText("Intermediate Pad")
    End Sub
    Private Sub Button100_MouseLeave(sender As Object, e As EventArgs) Handles Button100.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddinterToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Intermediate Pad '", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button98_Click(sender As Object, e As EventArgs) Handles Button98.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            Add12ToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("1/2 Pad Paper")
        Label5.Text = itemName
    End Sub
    Private Sub Button98_MouseEnter(sender As Object, e As EventArgs) Handles Button98.MouseEnter
        UpdateLinkLabelText("1/2 Pad Paper")
    End Sub
    Private Sub Button98_MouseLeave(sender As Object, e As EventArgs) Handles Button98.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Add12ToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = '1/2 Pad Paper'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button101_Click(sender As Object, e As EventArgs) Handles Button101.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddinToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Index Card")
        Label5.Text = itemName
    End Sub
    Private Sub Button101_MouseEnter(sender As Object, e As EventArgs) Handles Button101.MouseEnter
        UpdateLinkLabelText("Index Card")
    End Sub
    Private Sub Button101_MouseLeave(sender As Object, e As EventArgs) Handles Button101.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddinToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Index Card'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button146_Click(sender As Object, e As EventArgs) Handles Button146.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddcilToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Monggol 2 Pencil")
        Label5.Text = itemName
    End Sub
    Private Sub Button146_MouseEnter(sender As Object, e As EventArgs) Handles Button146.MouseEnter
        UpdateLinkLabelText("Monggol 2 Pencil")
    End Sub
    Private Sub Button146_MouseLeave(sender As Object, e As EventArgs) Handles Button146.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddcilToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Monggol 2 Pencil'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button147_Click(sender As Object, e As EventArgs) Handles Button147.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddblToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Ballpen (Black)")
        Label5.Text = itemName
    End Sub
    Private Sub Button147_MouseEnter(sender As Object, e As EventArgs) Handles Button147.MouseEnter
        UpdateLinkLabelText("Ballpen (Black)")
    End Sub
    Private Sub Button147_MouseLeave(sender As Object, e As EventArgs) Handles Button147.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddblToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Ballpen (Black)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button148_Click(sender As Object, e As EventArgs) Handles Button148.Click
        Dim quantityForm As New Quantity()

        If quantityForm.ShowDialog() = DialogResult.OK Then
            Dim quantity As Integer = quantityForm.Quantity
            AddrbToGrid(quantity)
        End If
        Dim itemName As String = GetItemNameFromDatabase("Ballpen (Red)")
        Label5.Text = itemName
    End Sub
    Private Sub Button148_MouseEnter(sender As Object, e As EventArgs) Handles Button148.MouseEnter
        UpdateLinkLabelText("Ballpen (Red)")
    End Sub
    Private Sub Button148_MouseLeave(sender As Object, e As EventArgs) Handles Button148.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub AddrbToGrid(quantity As Integer)
        Dim command As New MySqlCommand("SELECT Name, Price, Quantity AS AvailableStock FROM items WHERE Name = 'Ballpen (Red)'", conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        Try
            conn.Open()
            adapter.Fill(table)

            If quantity <= 0 Then
                MessageBox.Show("Please add a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If table.Rows.Count > 0 Then

                Dim itemName As String = table.Rows(0)("Name").ToString()
                Dim price As Decimal = Convert.ToDecimal(table.Rows(0)("Price"))
                Dim availableStock As Integer = Convert.ToInt32(table.Rows(0)("AvailableStock"))

                Dim totalQuantityInGrid As Integer = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        totalQuantityInGrid += Convert.ToInt32(row.Cells("Quantity").Value)
                    End If
                Next


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

                Dim found As Boolean = False
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("Name").Value IsNot Nothing AndAlso row.Cells("Name").Value.ToString() = itemName Then
                        Dim existingQuantity As Integer = Convert.ToInt32(row.Cells("Quantity").Value)
                        Dim newQuantity As Integer = existingQuantity + quantity
                        row.Cells("Quantity").Value = newQuantity
                        row.Cells("Total Price").Value = price * newQuantity
                        found = True
                        Exit For
                    End If
                Next

                If Not found Then
                    If DataGridView1.DataSource Is Nothing Then
                        Dim newTable As New DataTable()
                        newTable.Columns.Add("Name")
                        newTable.Columns.Add("Quantity")
                        newTable.Columns.Add("Price")
                        newTable.Columns.Add("Total Price")
                        DataGridView1.DataSource = newTable
                    End If
                    Dim newRow As DataRow = DirectCast(DataGridView1.DataSource, DataTable).NewRow()
                    newRow("Name") = itemName
                    newRow("Quantity") = quantity
                    newRow("Price") = price
                    newRow("Total Price") = totalPrice

                    DirectCast(DataGridView1.DataSource, DataTable).Rows.Add(newRow)
                    Dim dv As New DataView(DirectCast(DataGridView1.DataSource, DataTable))
                    dv.Sort = "Name ASC"
                    DataGridView1.DataSource = dv.ToTable()
                End If
                DataGridView1.AutoResizeColumns()
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

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim confirmationResult As DialogResult = MessageBox.Show("Are you sure you want to remove the selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmationResult = DialogResult.Yes Then
                ' Remove selected rows from DataGridView and deduct the total price
                For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
                    ' Check if the row is not a new row (uncommitted)
                    If Not selectedRow.IsNewRow Then
                        Dim totalPriceToRemove As Decimal = Convert.ToDecimal(selectedRow.Cells("Total Price").Value)
                        DataGridView1.Rows.Remove(selectedRow)
                        TotalPrice -= totalPriceToRemove ' Deduct the total price
                    End If
                Next
                UpdateTotalQuantityAndPrice() ' Update total quantity and total price display
            End If
        Else
            MessageBox.Show("Please select an item to remove.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim buttonsToFront() As Button = {Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51}

        For Each btn As Button In buttonsToFront
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Chips")
    End Sub

    Private Sub Button13_MouseEnter(sender As Object, e As EventArgs) Handles Button13.MouseEnter
        UpdateLinkLabelText("Chips")
    End Sub
    Private Sub Button13_MouseLeave(sender As Object, e As EventArgs) Handles Button13.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click

        Dim buttonsToShow() As Button = {Button13, Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51, Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66, Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81,
    Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129, Button130, Button131, Button133, Button134, Button135, Button136, Button137, Button138,
    Button139, Button140, Button141, Button142, Button143, Button144, Button145, Button132, Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161, Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button98, Button99, Button100, Button101, Button146, Button147, Button148, Button20, Button28, Button24, Button19, Button18, Button27, Button23, Button15, Button17, Button26, Button22, Button90, Button91, Button92}
        For Each btn As Button In buttonsToShow
            btn.Visible = True
            btn.Enabled = True
        Next
        'This part of the code shows the variables where when a specific button is clicked those buttons will be in front
        Dim chipsButtons() As Button = {Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51}
        Dim iceCreamButtons() As Button = {Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66}
        Dim BeveragesButtons() As Button = {Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81}
        Dim JampongButtons() As Button = {Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97}
        Dim StreetButtons() As Button = {Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113}
        Dim CandiesButtons() As Button = {Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129}
        Dim CrackersButtons() As Button = {Button130, Button131, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140, Button141, Button142, Button143, Button144, Button145, Button132}
        Dim SilogButtons() As Button = {Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161}
        Dim FolderButtons() As Button = {Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177}
        Dim EraserButtons() As Button = {Button90, Button91, Button92}
        Dim PaperButtons() As Button = {Button98, Button99, Button100, Button101}
        Dim BallpenButtons() As Button = {Button146, Button147, Button148}
        For Each btn As Button In chipsButtons
            btn.SendToBack()
        Next

        For Each btn As Button In iceCreamButtons
            btn.SendToBack()
        Next
        For Each btn As Button In BeveragesButtons
            btn.SendToBack()
        Next
        For Each btn As Button In JampongButtons
            btn.SendToBack()
        Next
        For Each btn As Button In StreetButtons
            btn.SendToBack()
        Next
        For Each btn As Button In CandiesButtons
            btn.SendToBack()
        Next
        For Each btn As Button In CrackersButtons
            btn.SendToBack()
        Next
        For Each btn As Button In SilogButtons
            btn.SendToBack()
        Next
        For Each btn As Button In FolderButtons
            btn.SendToBack()
        Next
        For Each btn As Button In EraserButtons
            btn.SendToBack()
        Next
        Label5.Text = "" ' Reset Label5 to blank
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Dim iceCreamButtons() As Button = {Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66}

        For Each btn As Button In iceCreamButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Ice Cream")
    End Sub

    Private Sub Button20_MouseEnter(sender As Object, e As EventArgs) Handles Button20.MouseEnter
        UpdateLinkLabelText("Ice Cream")
    End Sub
    Private Sub Button20_MouseLeave(sender As Object, e As EventArgs) Handles Button20.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim BeveragesButtons() As Button = {Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81}

        For Each btn As Button In BeveragesButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Drinks")
    End Sub

    Private Sub Button28_MouseEnter(sender As Object, e As EventArgs) Handles Button28.MouseEnter
        UpdateLinkLabelText("Drinks")
    End Sub
    Private Sub Button28_MouseLeave(sender As Object, e As EventArgs) Handles Button28.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click

        Dim buttonsToHide() As Button = {Button45, Button46, Button78, Button116, Button172, Button132, Button133, Button117, Button36, Button49, Button79, Button173, Button37, Button47, Button80, Button115, Button131, Button171, Button44, Button48, Button81, Button114, Button130, Button170}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next

        Dim JampongButtons() As Button = {Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97}
        For Each btn As Button In JampongButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Noodles")
    End Sub

    Private Sub Button24_MouseEnter(sender As Object, e As EventArgs) Handles Button24.MouseEnter
        UpdateLinkLabelText("Noodles")
    End Sub
    Private Sub Button24_MouseLeave(sender As Object, e As EventArgs) Handles Button24.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim CandiesButtons() As Button = {Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129}

        For Each btn As Button In CandiesButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Candies")
    End Sub

    Private Sub Button18_MouseEnter(sender As Object, e As EventArgs) Handles Button18.MouseEnter
        UpdateLinkLabelText("Candies")
    End Sub
    Private Sub Button18_MouseLeave(sender As Object, e As EventArgs) Handles Button18.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click

        Dim buttonsToHide() As Button = {Button45, Button46, Button78, Button116, Button172, Button132, Button133, Button117, Button36, Button49, Button79, Button173, Button37, Button47, Button80, Button115, Button131, Button171, Button44, Button48, Button81, Button114, Button130, Button170, Button93}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next


        Dim StreetButtons() As Button = {Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113}
        For Each btn As Button In StreetButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Street Foods")

    End Sub

    Private Sub Button19_MouseEnter(sender As Object, e As EventArgs) Handles Button19.MouseEnter
        UpdateLinkLabelText("Street Foods")
    End Sub
    Private Sub Button19_MouseLeave(sender As Object, e As EventArgs) Handles Button19.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim CrackersButtons() As Button = {Button130, Button131, Button132, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140, Button141, Button142, Button143, Button144, Button145}

        For Each btn As Button In CrackersButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Crackers Abd Breads")

    End Sub

    Private Sub Button27_MouseEnter(sender As Object, e As EventArgs) Handles Button27.MouseEnter
        UpdateLinkLabelText("Crackers And Breads")
    End Sub
    Private Sub Button27_MouseLeave(sender As Object, e As EventArgs) Handles Button27.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click

        Dim buttonsToHide() As Button = {Button45, Button46, Button78, Button116, Button172, Button132, Button133, Button117, Button36, Button49, Button79, Button173, Button37, Button47, Button80, Button115, Button131, Button171, Button44, Button48, Button81, Button114, Button130, Button170, Button93}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next

        Dim SilogButtons() As Button = {Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161}
        For Each btn As Button In SilogButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Meals")

    End Sub

    Private Sub Button23_MouseEnter(sender As Object, e As EventArgs) Handles Button23.MouseEnter
        UpdateLinkLabelText("Meals")
    End Sub
    Private Sub Button23_MouseLeave(sender As Object, e As EventArgs) Handles Button23.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim FolderButtons() As Button = {Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177}

        For Each btn As Button In FolderButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Folder")

    End Sub

    Private Sub Button15_MouseEnter(sender As Object, e As EventArgs) Handles Button15.MouseEnter
        UpdateLinkLabelText("Folder")
    End Sub
    Private Sub Button15_MouseLeave(sender As Object, e As EventArgs) Handles Button15.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

        Dim buttonsToHide() As Button = {Button13, Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51, Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66, Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81,
    Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129,
    Button130, Button131, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140, Button141, Button142, Button143, Button144, Button145, Button132, Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161,
    Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button98, Button99, Button100, Button101, Button146, Button147, Button148, Button20, Button28, Button24, Button19, Button18, Button27, Button23, Button15, Button17, Button26, Button22}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next

        Dim EraserButtons() As Button = {Button90, Button91, Button92}
        For Each btn As Button In EraserButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Eraser")

    End Sub

    Private Sub Button17_MouseEnter(sender As Object, e As EventArgs) Handles Button17.MouseEnter
        UpdateLinkLabelText("Eraser")
    End Sub
    Private Sub Button17_MouseLeave(sender As Object, e As EventArgs) Handles Button17.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click

        Dim buttonsToHide() As Button = {Button13, Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51, Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66, Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81,
    Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129,
    Button130, Button131, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140, Button141, Button142, Button143, Button144, Button145, Button132, Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161,
    Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button90, Button91, Button92, Button146, Button147, Button148, Button20, Button28, Button24, Button19, Button18, Button27, Button23, Button15, Button17, Button26, Button22}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next

        Dim EraserButtons() As Button = {Button90, Button91, Button92}
        For Each btn As Button In EraserButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Writing Materials")

    End Sub

    Private Sub Button26_MouseEnter(sender As Object, e As EventArgs) Handles Button26.MouseEnter
        UpdateLinkLabelText("Writing Materials")
    End Sub
    Private Sub Button26_MouseLeave(sender As Object, e As EventArgs) Handles Button26.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click

        Dim buttonsToHide() As Button = {Button13, Button29, Button32, Button33, Button34, Button35, Button36, Button37, Button38, Button39, Button40, Button41, Button42, Button43, Button44, Button45, Button51, Button30, Button46, Button47, Button48, Button49, Button50, Button52, Button57, Button58, Button59, Button60, Button61, Button62, Button63, Button64, Button66, Button56, Button67, Button68, Button69, Button70, Button71, Button72, Button73, Button74, Button75, Button76, Button77, Button78, Button79, Button80, Button81,
    Button82, Button83, Button84, Button85, Button86, Button87, Button88, Button89, Button93, Button94, Button95, Button96, Button97, Button102, Button103, Button104, Button105, Button106, Button107, Button108, Button109, Button110, Button111, Button112, Button113, Button114, Button115, Button116, Button117, Button118, Button119, Button120, Button121, Button122, Button123, Button124, Button125, Button126, Button127, Button128, Button129,
    Button130, Button131, Button133, Button134, Button135, Button136, Button137, Button138, Button139, Button140, Button141, Button142, Button143, Button144, Button145, Button132, Button150, Button151, Button152, Button153, Button154, Button155, Button156, Button157, Button158, Button159, Button160, Button161,
    Button162, Button163, Button164, Button165, Button166, Button167, Button168, Button169, Button170, Button171, Button172, Button173, Button174, Button175, Button176, Button177, Button90, Button91, Button92, Button100, Button99, Button98, Button101, Button20, Button28, Button24, Button19, Button18, Button27, Button23, Button15, Button17, Button26, Button22}
        For Each btn As Button In buttonsToHide
            btn.Visible = False
            btn.Enabled = False
        Next

        Dim EraserButtons() As Button = {Button90, Button91, Button92}
        For Each btn As Button In EraserButtons
            btn.BringToFront()
        Next
        UpdateLinkLabelText("Pencil And Ballpen")

    End Sub

    Private Sub Button22_MouseEnter(sender As Object, e As EventArgs) Handles Button22.MouseEnter
        UpdateLinkLabelText("Pencil And Ballpen")
    End Sub
    Private Sub Button22_MouseLeave(sender As Object, e As EventArgs) Handles Button22.MouseLeave
        UpdateLinkLabelText("")
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim currentQuantity As Integer = Convert.ToInt32(selectedRow.Cells("Quantity").Value)

            If currentQuantity > 0 Then
                Dim reductionForm As New QuantityReduction()
                reductionForm.MaxQuantity = currentQuantity
                If reductionForm.ShowDialog() = DialogResult.OK Then
                    Dim reductionAmount As Integer = reductionForm.ReductionAmount
                    If reductionAmount > 0 Then
                        Dim newQuantity As Integer = currentQuantity - reductionAmount
                        If newQuantity < 1 Then
                            MessageBox.Show("The Quantity input exceeds the quantity of the item.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Else
                            selectedRow.Cells("Quantity").Value = newQuantity

                            ' Update the total price by subtracting the price of the reduced quantity
                            Dim price As Decimal = Convert.ToDecimal(selectedRow.Cells("Price").Value)
                            Dim totalPrice As Decimal = Convert.ToDecimal(selectedRow.Cells("Total Price").Value)
                            selectedRow.Cells("Total Price").Value = totalPrice - (price * reductionAmount)

                            ' Update Label3 to reflect the reduced quantity
                            totalPrice -= price * reductionAmount
                            UpdateTotalQuantityAndPrice() ' Update total quantity and total price display
                        End If
                    Else
                        MessageBox.Show("Please enter a valid reduction amount.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If

            End If
        Else
            MessageBox.Show("Please select an item to reduce quantity.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function GetItemNameFromDatabase(itemName As String) As String
        Dim command As New MySqlCommand("SELECT Name FROM items WHERE Name = @itemName", conn)
        command.Parameters.AddWithValue("@itemName", itemName)
        Dim itemNameFromDB As String = ""

        Try
            conn.Open()
            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                itemNameFromDB = result.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

        Return itemNameFromDB
    End Function
    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click

        ' Check if there are any rows in the DataGridView
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Please select an item first before checking out.")
            Return ' Exit the event handler if DataGridView is empty
        End If

        ' Check if the current row is not null
        If DataGridView1.CurrentRow IsNot Nothing Then
            ' Retrieve the information of the selected item from the DataGridView
            Dim selectedItemName As String = Convert.ToString(DataGridView1.CurrentRow.Cells("Name").Value)
            Dim selectedQuantity As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells("Quantity").Value)

            ' Update the quantity of the item in the database
            Dim connStr As String = "Server=localhost;Database=inventory;Uid=root;Pwd="
            Using connection As New MySqlConnection(connStr)
                connection.Open()
                Dim commandText As String = "UPDATE items SET Quantity = Quantity - @Quantity WHERE Name = @Name"
                Using command As New MySqlCommand(commandText, connection)
                    command.Parameters.AddWithValue("@Quantity", selectedQuantity)
                    command.Parameters.AddWithValue("@Name", selectedItemName)
                    command.ExecuteNonQuery()
                End Using
            End Using

            ' Pass the value of Label3.Text to the PayForm
            Dim label3Value As String = Label3.Text
            Dim payForm As New Pay(label3Value)
            payForm.Show()
        Else
            MessageBox.Show("Please select an item before checking out.")
        End If
    End Sub

  
End Class
Imports MySql.Data.MySqlClient
Imports Mysqlx
Imports System.Data
Imports System.Globalization

Public Class Manage

    Dim conn As New MySqlConnection("Data source=localhost;database=inventory;username=root;password=") 'connection to the database
    Dim dataTable As New DataTable()
    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        ' Cancel the edit operation
        e.Cancel = True
    End Sub

    Private Sub TxtName_GotFocus(sender As Object, e As EventArgs) Handles txtName.GotFocus
        ' Clear the shadow text when textbox is focused
        If txtName.Text = "Enter Item" Then
            txtName.Text = ""
            txtName.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub TxtName_LostFocus(sender As Object, e As EventArgs) Handles txtName.LostFocus
        ' Restore the shadow text if textbox is empty
        If txtName.Text.Trim() = "" Then
            SetShadowText(txtName, "Enter Item", Color.Gray)
        End If
    End Sub

    Private Sub TxtQuantity_GotFocus(sender As Object, e As EventArgs) Handles TxtQuantity.GotFocus
        ' Clear the shadow text when textbox is focused
        If TxtQuantity.Text = "Enter Quantity" Then
            TxtQuantity.Text = ""
            TxtQuantity.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub TxtQuantity_LostFocus(sender As Object, e As EventArgs) Handles TxtQuantity.LostFocus
        ' Restore the shadow text if textbox is empty
        If TxtQuantity.Text.Trim() = "" Then
            SetShadowTextQuantity(TxtQuantity, "Enter Quantity", Color.Gray)
        End If
    End Sub

    Private Sub TxtPrice_GotFocus(sender As Object, e As EventArgs) Handles TxtPrice.GotFocus
        ' Clear the shadow text when textbox is focused
        If TxtPrice.Text = "Enter Price" Then
            TxtPrice.Text = ""
            TxtPrice.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub TxtPrice_LostFocus(sender As Object, e As EventArgs) Handles TxtPrice.LostFocus
        ' Restore the shadow text if textbox is empty
        If TxtPrice.Text.Trim() = "" Then
            SetShadowTextPrice(TxtPrice, "Enter Price", Color.Gray)
        End If
    End Sub

    Private Sub TxtQuantityUpdate_GotFocus(sender As Object, e As EventArgs) Handles TxtQuantityUpdate.GotFocus
        ' Clear the shadow text when textbox is focused
        If TxtQuantityUpdate.Text = "Quantity Update" Then
            TxtQuantityUpdate.Text = ""
            TxtQuantityUpdate.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub TxtQuantityUpdate_LostFocus(sender As Object, e As EventArgs) Handles TxtQuantityUpdate.LostFocus
        ' Restore the shadow text if textbox is empty
        If TxtQuantityUpdate.Text.Trim() = "" Then
            SetShadowTextQuantityUpdate(TxtQuantityUpdate, "Quantity Update", Color.Gray)
        End If
    End Sub

    Private Sub TxtPriceUpdate_GotFocus(sender As Object, e As EventArgs) Handles TxtPriceUpdate.GotFocus
        ' Clear the shadow text when textbox is focused
        If TxtPriceUpdate.Text = "Price Update" Then
            TxtPriceUpdate.Text = ""
            TxtPriceUpdate.ForeColor = Color.Black ' Change text color to black when active
        End If
    End Sub

    Private Sub TxtPriceUpdate_LostFocus(sender As Object, e As EventArgs) Handles TxtPriceUpdate.LostFocus
        ' Restore the shadow text if textbox is empty
        If TxtPriceUpdate.Text.Trim() = "" Then
            SetShadowTextPriceUpdate(TxtPriceUpdate, "Price Update", Color.Gray)
        End If
    End Sub

    Private Sub SetShadowText(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub

    Private Sub SetShadowTextQuantity(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub

    Private Sub SetShadowTextPrice(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub

    Private Sub SetShadowTextQuantityUpdate(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub

    Private Sub SetShadowTextPriceUpdate(textBox As TextBox, shadowText As String, shadowColor As Color)
        textBox.Text = shadowText
        textBox.ForeColor = shadowColor
    End Sub


    Private Sub Manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.RowHeadersVisible = False
        lblname2.Text = Adminlog.txtusername.Text ' This line of code shows the username of the user
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        txtName.ForeColor = Color.Gray
        txtName.Text = "Enter Item"

        TxtQuantity.ForeColor = Color.Gray
        TxtQuantity.Text = "Enter Quantity"

        TxtPrice.ForeColor = Color.Gray
        TxtPrice.Text = "Enter Price"

        TxtQuantityUpdate.ForeColor = Color.Gray
        TxtQuantityUpdate.Text = "Quantity Update"

        TxtPriceUpdate.ForeColor = Color.Gray
        TxtPriceUpdate.Text = "Price Update"

        LoadInventory()
        SortDataGridViewByName()
    End Sub
    Private Sub LoadInventory()
        Dim query As String = "SELECT * FROM Items"
        Dim adapter As New MySqlDataAdapter(query, conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
        End Sub
    Private Sub SortDataGridViewByName()
        ' Check if the DataGridView has any rows
        If DataGridView1.Rows.Count > 0 Then
            ' Sort the DataGridView by the "Name" column in ascending order
            DataGridView1.Sort(DataGridView1.Columns("Name"), System.ComponentModel.ListSortDirection.Ascending)
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim itemName As String = txtName.Text.Trim()
        Dim quantityText As String = TxtQuantity.Text.Trim()
        Dim priceText As String = TxtPrice.Text.Trim()

        ' Check if all required textboxes are filled
        If itemName = "" OrElse quantityText = "" OrElse priceText = "" Then
            MessageBox.Show("Please fill all the required textboxes.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Convert the first letter of the item name to uppercase
        itemName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(itemName.ToLower())

        ' Check if the item already exists in the database
        If ItemExists(itemName) Then
            MessageBox.Show("This item already exists.", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            txtName.Text = "Enter Item"
            txtName.ForeColor = Color.Gray
            TxtQuantity.Text = "Enter Quantity"
            TxtQuantity.ForeColor = Color.Gray
            TxtPrice.Text = "Enter Price"
            TxtPrice.ForeColor = Color.Gray

            Return
        End If

        ' Proceed with the rest of the validation
        Dim quantity As Integer
        If Integer.TryParse(quantityText, quantity) Then
            Dim price As Double
            If Double.TryParse(priceText, price) Then
                ' Price input is valid
                Dim query As String = "INSERT INTO Items (Name, Quantity, Price) VALUES (@Name, @Quantity, @Price)"
                Dim command As New MySqlCommand(query, conn)
                command.Parameters.AddWithValue("@Name", itemName)
                command.Parameters.AddWithValue("@Quantity", quantity)
                command.Parameters.AddWithValue("@Price", price)

                conn.Open()
                command.ExecuteNonQuery()
                conn.Close()

                txtName.Text = "Enter Item"
                txtName.ForeColor = Color.Gray
                TxtQuantity.Text = "Enter Quantity"
                TxtQuantity.ForeColor = Color.Gray
                TxtPrice.Text = "Enter Price"
                TxtPrice.ForeColor = Color.Gray

                ' Clear the text of the textboxes
                txtName.Text = "Enter Item"
                txtName.ForeColor = Color.Gray
                TxtQuantity.Text = "Enter Quantity"
                TxtQuantity.ForeColor = Color.Gray
                TxtPrice.Text = "Enter Price"
                TxtPrice.ForeColor = Color.Gray

                LoadInventory()
            Else
                MessageBox.Show("Please enter a valid Value.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Please enter a valid Value.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function ItemExists(ByVal itemName As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM Items WHERE Name = @Name"
        Dim command As New MySqlCommand(query, conn)
        command.Parameters.AddWithValue("@Name", itemName)

        conn.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
        conn.Close()

        Return count > 0
    End Function





    Private Sub BtnRemove_Click(sender As Object, e As EventArgs) Handles BtnRemove.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Show confirmation dialog
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ' If user confirms deletion
            If result = DialogResult.Yes Then
                Dim selectedID As Integer = DataGridView1.SelectedRows(0).Cells("ID").Value

                Dim query As String = "DELETE FROM Items WHERE ID = @ID"
                Dim command As New MySqlCommand(query, conn)
                command.Parameters.AddWithValue("@ID", selectedID)

                conn.Open()
                command.ExecuteNonQuery()
                conn.Close()

                LoadInventory()
            End If
        End If
    End Sub
    Private Sub BtnUpdateQuantity_Click_1(sender As Object, e As EventArgs) Handles BtnUpdateQuantity.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the ID of the selected item
            Dim selectedID As Integer = DataGridView1.SelectedRows(0).Cells("ID").Value

            ' Validate the input in the Quantity Update textbox
            Dim additionalQuantity As Integer
            If Integer.TryParse(TxtQuantityUpdate.Text.Trim(), additionalQuantity) AndAlso additionalQuantity > 0 Then
                ' Construct the SQL query to update the quantity
                Dim query As String = "UPDATE Items SET Quantity = Quantity + @AdditionalQuantity WHERE ID = @ID"

                ' Create a MySqlCommand object with the query and parameters
                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@AdditionalQuantity", additionalQuantity)
                    command.Parameters.AddWithValue("@ID", selectedID)

                    ' Open the connection, execute the command, and close the connection
                    conn.Open()
                    command.ExecuteNonQuery()
                    conn.Close()

                    ' Refresh the DataGridView to reflect the updated data
                    LoadInventory()

                    ' Clear the Quantity Update textbox
                    TxtQuantityUpdate.Text = ""

                    ' Show the shadow text
                    SetShadowTextQuantityUpdate(TxtQuantityUpdate, "Quantity Update", Color.Gray)
                End Using
            Else
                MessageBox.Show("Please enter a valid positive integer in the Quantity Update textbox.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Please select an item from the inventory to update its quantity.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub BtnUpdatePrice_Click(sender As Object, e As EventArgs) Handles btnUpdatePrice.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the ID of the selected item
            Dim selectedID As Integer = DataGridView1.SelectedRows(0).Cells("ID").Value

            ' Validate the input in the Price Update textbox
            Dim newPrice As Double
            If Double.TryParse(TxtPriceUpdate.Text.Trim(), newPrice) AndAlso newPrice > 0 Then
                ' Construct the SQL query to update the price
                Dim query As String = "UPDATE Items SET Price = @NewPrice WHERE ID = @ID"

                ' Create a MySqlCommand object with the query and parameters
                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@NewPrice", newPrice)
                    command.Parameters.AddWithValue("@ID", selectedID)

                    ' Open the connection, execute the command, and close the connection
                    conn.Open()
                    command.ExecuteNonQuery()
                    conn.Close()

                    ' Refresh the DataGridView to reflect the updated data
                    LoadInventory()

                    ' Clear the Price Update textbox
                    TxtPriceUpdate.Text = ""

                    ' Show the shadow text
                    SetShadowTextPriceUpdate(TxtPriceUpdate, "Price Update", Color.Gray)
                End Using
            Else
                MessageBox.Show("Please enter a valid number in the Price Update textbox.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Please select an item from the inventory to update its price.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim ManageSales As New ManageSales()
        ManageSales.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Dashboard As New Dashboard()
        Dashboard.Show()
        Me.Close()
    End Sub

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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Records As New Records()
        Records.Show()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim searchKeyword As String = TextBox1.Text.Trim()

        ' If the search keyword is empty, reload the entire inventory
        If searchKeyword = "" Then
            LoadInventory()
        Else
            ' Construct the SQL query to search for items matching the entered keyword
            Dim query As String = "SELECT * FROM Items WHERE Name LIKE @Keyword OR ID LIKE @Keyword"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@Keyword", "%" & searchKeyword & "%")
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        End If
    End Sub



End Class



Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Public Class Records

    Dim conn As New MySqlConnection("Data source=localhost;database=pos;username=root;password=") 'connection to the database

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        ' Check if the current cell is of type DataGridViewComboBoxCell
        If TypeOf e.Control Is ComboBox Then
            Dim comboBox As ComboBox = TryCast(e.Control, ComboBox)

            ' Disable dropdown by setting the cell's ReadOnly property to true
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList

        End If
    End Sub
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Columns(e.ColumnIndex).Name = "user_photos" AndAlso e.RowIndex >= 0 Then
            Dim cell As DataGridViewImageCell = CType(DataGridView1.Rows(e.RowIndex).Cells("user_photos"), DataGridViewImageCell)
            cell.ImageLayout = DataGridViewImageCellLayout.Zoom ' Set to Zoom for stretching/fitting
        End If
    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        ' Cancel the edit operation
        e.Cancel = True
    End Sub
    Private Sub Records_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.RowHeadersVisible = False
        ' Set edit mode to EditProgrammatically
        DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill


        lblname2.Text = Adminlog.txtusername.Text 'this line Of code shows the username Of the user

        ' SQL query to select all records from the items table
        Dim query As String = "SELECT * FROM user"

        ' Create a new MySqlConnection using the connection string

        Try
            ' Open the connection
            conn.Open()

            ' Create a new MySqlCommand with the SQL query and connection
            Using command As New MySqlCommand(query, conn)
                ' Create a new MySqlDataAdapter to retrieve the data
                Using adapter As New MySqlDataAdapter(command)
                    ' Create a new DataTable to hold the data
                    Dim dataTable As New DataTable()

                    ' Fill the DataTable with the data from the adapter
                    adapter.Fill(dataTable)

                    ' Bind the DataTable to the DataGridView to display the data
                    DataGridView1.DataSource = dataTable
                End Using
            End Using
        Catch ex As Exception
            ' Display an error message if there is an exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim UserReg As New UserReg()
        UserReg.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Dashboard As New Dashboard()
        Dashboard.Show()
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        For Each row As DataGridViewRow In DataGridView1.Rows
            ' Access the cell containing the image (assuming it's in the "user_photos" column)
            Dim cell As DataGridViewImageCell = TryCast(row.Cells("user_photos"), DataGridViewImageCell)

            ' Set the image layout to stretch
            If cell IsNot Nothing Then
                cell.ImageLayout = DataGridViewImageCellLayout.Stretch
            End If
        Next
    End Sub

End Class
Imports MySql.Data.MySqlClient
Imports Mysqlx
Imports System.Windows.Forms.DataVisualization.Charting
Public Class ManageSales

    Dim connection As MySqlConnection = New MySqlConnection("Data source=localhost;database=inventory;username=root;password=")

        Private Sub ManageSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblname2.Text = Adminlog.txtusername.Text ' This line of code shows the username of the user
        ' Initialize the chart with a series
        InitializeChart()

            ' Load and display sorted data from the database
            LoadChartData()
        End Sub

        Private Sub InitializeChart()
            ' Create a new series
            Dim quantitySeries As New Series("Quantity")

            ' Set the chart type for the series
            quantitySeries.ChartType = SeriesChartType.Column

            ' Add the series to the chart
            Chart1.Series.Add(quantitySeries)
        End Sub

        Private Sub LoadChartData()
            Try
                Using connection As New MySqlConnection("Data source=localhost;database=inventory;username=root;password=")
                    connection.Open()

                ' Retrieve data from the items table and order by Quantity
                Dim query As String = "SELECT Name, Quantity FROM items ORDER BY Quantity  LIMIT 5"
                Using cmd As New MySqlCommand(query, connection)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim itemName As String = reader("Name").ToString()
                                Dim quantity As Integer = Convert.ToInt32(reader("Quantity"))

                                ' Add data to the "Quantity" series in the chart
                                Chart1.Series("Quantity").Points.AddXY(itemName, quantity)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading data: " & ex.Message)
            End Try
        End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Dashboard As New Dashboard()
        Dashboard.Show()
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        lblname2.Text = Adminlog.txtusername.Text ' This line of code shows the username of the user
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
End Class


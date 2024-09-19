Imports MySql.Data.MySqlClient
Module Module1
    Public con As New MySqlConnection
    'this code is for the connection testing
    Sub opencon()
        con.ConnectionString = "server=localhost;username=root;password=;database=pos"
        con.Open()

    End Sub
End Module

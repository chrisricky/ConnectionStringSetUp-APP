Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim datasource As String = txtDatasource.Text
        Dim initialCatalog As String = txtInitialCatalog.Text
        Dim userID As String = txtUserID.Text
        Dim password As String = txtPassword.Text

        ' Concatenate the input values to form the connection string
        Dim plainTextConnectionString As String = $"Data Source={datasource};Initial Catalog={initialCatalog};User ID={userID};Password={password};"

        ' Encrypt the connection string
        Dim encryptedConnectionString As String = EncryptionHelper.Encrypt(plainTextConnectionString)

        ' Get the current value of the environment variable (if it exists)
        Dim currentEnvVar As String = Environment.GetEnvironmentVariable("MY_CONNECTION_STRING")

        ' Check if the environment variable exists and if it's different from the new encrypted connection string
        If String.IsNullOrEmpty(currentEnvVar) OrElse currentEnvVar <> encryptedConnectionString Then
            ' Set the environment variable
            Environment.SetEnvironmentVariable("MY_CONNECTION_STRING", encryptedConnectionString, EnvironmentVariableTarget.User)

            ' Register a startup script to show a JavaScript alert
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Environment variable set.');", True)

        End If
        txtDatasource.Text = ""
        txtInitialCatalog.Text = ""
        txtUserID.Text = ""
        txtPassword.Text = ""

    End Sub
End Class
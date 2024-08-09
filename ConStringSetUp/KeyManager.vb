Imports System.Security.Cryptography
Imports System.IO

Public Class KeyManager
    ' Path to key files
    Private Const PublicKeyFileName As String = "public_key.xml"
    Private Const PrivateKeyFileName As String = "private_key.xml"

    ' Get the path to the key files in the user's Local Application Data directory
    Public Shared ReadOnly Property PublicKeyPath As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestWebApp", PublicKeyFileName)
        End Get
    End Property

    Public Shared ReadOnly Property PrivateKeyPath As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestWebApp", PrivateKeyFileName)
        End Get
    End Property

    ' Ensure that RSA keys exist
    Public Shared Sub EnsureKeysExist()
        ' Get the directory path where the key files will be stored
        Dim directoryPath As String = Path.GetDirectoryName(PublicKeyPath)

        ' Create the directory if it does not exist
        If Not Directory.Exists(directoryPath) Then
            Directory.CreateDirectory(directoryPath)
        End If

        ' Check if the key files exist and generate them if they do not
        If Not File.Exists(PublicKeyPath) OrElse Not File.Exists(PrivateKeyPath) Then
            GenerateKeys()
        End If
    End Sub

    ' Generate RSA keys and save them to XML files
    Private Shared Sub GenerateKeys()
        Using rsa As New RSACryptoServiceProvider(2048)
            ' Export the public key to XML format
            Dim publicKeyXml As String = rsa.ToXmlString(False) ' False for public key only
            File.WriteAllText(PublicKeyPath, publicKeyXml)

            ' Export the private key to XML format
            Dim privateKeyXml As String = rsa.ToXmlString(True) ' True for private key
            File.WriteAllText(PrivateKeyPath, privateKeyXml)
        End Using
    End Sub
End Class

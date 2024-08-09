Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class EncryptionHelper
    ' Read the RSA keys from XML files
    Private Shared ReadOnly PublicKeyXml As String = File.ReadAllText(KeyManager.PublicKeyPath)
    Private Shared ReadOnly PrivateKeyXml As String = File.ReadAllText(KeyManager.PrivateKeyPath)

    Public Shared Function Encrypt(plainText As String) As String
        Dim plainBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
        Using rsa As New RSACryptoServiceProvider()
            rsa.FromXmlString(PublicKeyXml)
            Dim encryptedBytes As Byte() = rsa.Encrypt(plainBytes, False)
            Return Convert.ToBase64String(encryptedBytes)
        End Using
    End Function

    Public Shared Function Decrypt(cipherText As String) As String
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using rsa As New RSACryptoServiceProvider()
            rsa.FromXmlString(PrivateKeyXml)
            Dim decryptedBytes As Byte() = rsa.Decrypt(cipherBytes, False)
            Return Encoding.UTF8.GetString(decryptedBytes)
        End Using
    End Function
End Class

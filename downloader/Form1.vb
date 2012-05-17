Imports System.Net
Imports System.Text
Imports System.IO
Public Class Form1


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try
            Dim PHPURL As String '从此地址获取文件列表
            Dim DownURL As String 'Storage根目录
            DownURL = URLbox.Text.ToString
            PHPURL = PHPURLbox.Text.ToString + "?storeapp=" + StoreApp.Text.ToString
            ' Initialize the WebRequest.
            Dim myRequest As WebRequest = WebRequest.Create(PHPURL)
            Dim myResponse As WebResponse = myRequest.GetResponse()
            Dim reader As StreamReader = New StreamReader(myResponse.GetResponseStream)
            Dim FileName As String
            FileName = reader.ReadToEnd
            Dim DownFileName() As String
            DownFileName = FileName.Split("|")
            Dim myWebClient As New WebClient
            Dim i As Integer
            Try
                For i = 0 To DownFileName.Length - 3
                    Dim temp As String() = New String() {}
                    temp = DownFileName(i).Split("/")
                    checkdirectory(temp)
                    myWebClient.DownloadFile(DownURL + "/" + DownFileName(i), DownFileName(i))
                    ProgressBar1.Value = (i / (DownFileName.Length - 3)) * 100
                Next
            Catch ex As Exception
                MessageBox.Show("请检查是否已经正确输入Storage地址和PHP地址")
            End Try
            If (DownFileName(DownFileName.Length - 2) = "FILE DOWN OVER") Then
                MessageBox.Show("总文件个数为:" + DownFileName(DownFileName.Length - 1) + vbCrLf + "下载已经完成!")
            Else
                MessageBox.Show("发生未知错误,部分文件可能下载失败")
            End If
        Catch ex As Exception
            MessageBox.Show("发生未知错误，请检查您的网络和输入状态是否正常后重试！")
        End Try
      


    End Sub
    Private Sub checkdirectory(temp As String())
        Try
            Dim path As String
            path = temp(0)
            If temp.Length = 1 Then
                Return
            End If
            Dim i As Integer
            i = 0
            For i = 1 To temp.Length - 1
                If (i < temp.Length - 1) And temp.Length > 1 Then
                    path = path + "/" + temp(i)
                End If
            Next
            
            MessageBox.Show(path)
            Try
                If (Directory.Exists(path) = False) Then
                    Directory.CreateDirectory(path)
                End If
            Catch ex As Exception
                MessageBox.Show("创建文件夹时发生了一些错误，请手动创建与Storage相同的目录结构重试")
            End Try
        Catch ex As Exception
            MessageBox.Show("Error")
        End Try
        

    End Sub

End Class

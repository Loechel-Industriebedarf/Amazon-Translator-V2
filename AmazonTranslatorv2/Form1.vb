Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.FileIO
Imports System.Threading

Public Class Form1

    Dim srcDirectory As String = "C:\Users\Administrator\amtu2\DocumentTransport\production\reports\"
    Dim doneReports As String = "C:\Users\Administrator\amtu2\DocumentTransport\production\reports\done\"
    Dim destDirectory As String = "C:\eNVenta-ERP\BMECat\Amazon\"
    Dim logDirectory As String = "C:\eNVenta-ERP\BMECat\Amazon\log\"
    Dim lastFilePath As String = "C:\eNVenta-ERP\BMECat\Amazon\log\lastFile.txt"
    Dim orderName As String = "ORDER*.txt"
    Dim filename As String = "AmazonOrders.csv"
    Dim logFile As String = "log.log"
    Dim fileExists As Boolean = False



    Dim text_file_path As String = ""
    Dim text_file_path_new As String = ""
    Dim file_dest As String = ""
    Dim file_dest_logAll As String = ""
    Dim file_old As String = ""
    Dim file_data As String

    Dim file_text As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Shown

        Dim thread As New Thread(AddressOf mainFunction)

        thread.Start()

    End Sub


    Private Sub mainFunction()


        While (True)
            While (True)
                Try
                    IO.File.AppendAllText(logDirectory + logFile, String.Format("{0}{1}", Environment.NewLine, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + "[S] | Process started"))
                Catch ex As Exception

                End Try

                ' What's the latest report that got written to the system?
                Dim lastFile As String = My.Computer.FileSystem.ReadAllText(lastFilePath)

                ' Read latest amazon report
                Dim dirs As String() = Directory.GetFiles(srcDirectory, orderName)

                file_dest = destDirectory + filename

                ' If there is a file with 0kb, delete it, it will only make problems else
                If File.Exists(file_dest) Then
                    If FileLen(file_dest) <= 0 Then
                        File.Delete(file_dest)
                    End If
                End If

                ' If the file already exists or if there are no new reports: exit the application
                If File.Exists(file_dest) Then
                    Try
                        IO.File.AppendAllText(logDirectory + logFile, String.Format("{0}{1}", Environment.NewLine, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + "[W] | File already exists." + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + "Waiting 5 minutes."))
                    Catch ex As Exception

                    End Try
                    Threading.Thread.Sleep(300000) ' 5 minutes
                    Exit While
                End If




                Dim data As System.IO.StreamWriter

                ' Generate CSV file
                Dim wordArr As String() = lastFile.Split("\")
                Dim result As String = wordArr(1)
                Dim doneReportsFile As String = doneReports + wordArr(wordArr.Length - 1)
                Try
                    File.Copy(lastFile, doneReportsFile, True)
                    File.Delete(lastFile)
                Catch ex As Exception
                    Console.WriteLine(ex)
                End Try




                ' Read latest amazon report
                ' If there are no files, exit the application
                Try
                    dirs = Directory.GetFiles(srcDirectory, orderName)
                    text_file_path = dirs(0)
                Catch ex As Exception
                    Try
                        IO.File.AppendAllText(logDirectory + logFile, String.Format("{0}{1}", Environment.NewLine, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + "[W] | No reports left." + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + "Waiting 15 minutes."))
                    Catch ex2 As Exception

                    End Try
                    Threading.Thread.Sleep(900000) ' 15 minutes
                    Exit While
                End Try

                data = My.Computer.FileSystem.OpenTextFileWriter(file_dest, False)


                ' Read stuff from txt file
                Dim tfp As New TextFieldParser(text_file_path, Encoding.GetEncoding(1252))  ' Source files are in ANSI-encoding
                tfp.Delimiters = New String() {vbTab}
                tfp.TextFieldType = FieldType.Delimited

                Dim old_order As String = ""
                tfp.ReadLine() 'Skips header
                While tfp.EndOfData = False
                    file_data = tfp.ReadLine()
                    file_data = Replace(file_data, ";", ",")
                    file_data = Replace(file_data, vbTab, ";")
                    Dim file_data_array As String() = file_data.Split(New Char() {";"c})

                    file_data = ""
                    For value As Integer = 0 To file_data_array.Length - 1
                        ' Changes Parameters to shipping parameters
                        Select Case value
                            Case 11
                                ' Fix for prices
                                ' Current price divided by number of articles
                                Dim new_price As Double = Convert.ToDouble(file_data_array(value)) / 100 / Convert.ToDouble(file_data_array(9))
                                Dim new_price_string As String = Replace(new_price.ToString, ",", ".")   ' We need a . for decimals
                                file_data = file_data + new_price_string + ";"
                            Case 17
                                    ' Do Stuff for 17 in step 18
                            Case 18
                                ' User is private, not business
                                If file_data_array(value).Length = 0 Then
                                    file_data = file_data + file_data_array(value - 1) + ";;"
                                Else
                                    file_data = file_data + file_data_array(value) + ";" + file_data_array(value - 1) + ";"
                                End If
                            Case 25
                                    ' Do Stuff for 17 in step 18
                            Case 26
                                ' User is private, not business
                                If file_data_array(value).Length = 0 Then
                                    file_data = file_data + file_data_array(value - 1) + ";;"
                                Else
                                    file_data = file_data + file_data_array(value) + ";" + file_data_array(value - 1) + ";"
                                End If
                            Case Else
                                file_data = file_data + file_data_array(value) + ";"
                        End Select
                    Next

                    data.WriteLine(file_data)
                    ' Shipping shouldn't be calculated two times
                    If String.Compare(old_order, file_data_array(0)) Then
                        Dim shipping As String = ""
                        For value As Integer = 0 To file_data_array.Length - 1
                            ' Changes Parameters to shipping parameters
                            Select Case value
                                Case 7
                                    shipping = shipping + "VERSAND-1955_LAGER" + ";"    ' Shipping sku
                                Case 8
                                    shipping = shipping + "Versand Amazon" + ";"        ' Shipping name
                                Case 9
                                    shipping = shipping + "1" + ";"                     ' Numbers of shippings
                                Case 11
                                    shipping = shipping + "4.9" + ";"                   ' Shipping cost
                                Case 17
                                    ' Do Stuff for 17 in step 18
                                Case 18
                                    ' User is private, not business
                                    If file_data_array(value).Length = 0 Then
                                        shipping = shipping + file_data_array(value - 1) + ";;"
                                    Else
                                        shipping = shipping + file_data_array(value) + ";" + file_data_array(value - 1) + ";"
                                    End If
                                Case 25
                                    ' Do Stuff for 17 in step 18
                                Case 26
                                    ' User is private, not business
                                    If file_data_array(value).Length = 0 Then
                                        shipping = shipping + file_data_array(value - 1) + ";;"
                                    Else
                                        shipping = shipping + file_data_array(value) + ";" + file_data_array(value - 1) + ";"
                                    End If
                                Case Else
                                    shipping = shipping + file_data_array(value) + ";"
                            End Select
                        Next
                        ' Write shipping to file
                        data.WriteLine(shipping)
                    End If

                    old_order = file_data_array(0)
                End While

                ' Write the name of the last read file to txt
                Dim lastFileWriter As System.IO.StreamWriter
                lastFileWriter = My.Computer.FileSystem.OpenTextFileWriter(lastFilePath, False)
                lastFileWriter.Write(text_file_path.ToString)
                lastFileWriter.Close()

                data.Close()

                Try
                    IO.File.AppendAllText(logDirectory + logFile, String.Format("{0}{1}", Environment.NewLine, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " " + "[R] | Successfully read report!" + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + Convert.ToChar(Keys.Tab) + "Waiting 1 minute."))
                Catch ex As Exception

                End Try
                Threading.Thread.Sleep(60000) ' 1 minute
            End While
        End While
    End Sub

    Private Sub openReports_Click(sender As Object, e As EventArgs) Handles openReports.Click
        Process.Start(srcDirectory)
    End Sub

    Private Sub openAmazon_Click(sender As Object, e As EventArgs) Handles openAmazon.Click
        Process.Start(destDirectory)
    End Sub

    Private Sub openLog_Click(sender As Object, e As EventArgs) Handles openLog.Click
        Process.Start(logDirectory + logFile)
    End Sub

    Private Sub openDoneReports_Click(sender As Object, e As EventArgs) Handles openDoneReports.Click
        Process.Start(doneReports)
    End Sub

    Private Sub openLogFolder_Click(sender As Object, e As EventArgs) Handles openLogFolder.Click
        Process.Start(logDirectory)
    End Sub

    Private Sub openDebug_Click(sender As Object, e As EventArgs) Handles openDebug.Click
        Process.Start("Z:\Automatisierung\Projects\AmazonTranslatorv2\AmazonTranslatorv2\bin\Debug")
    End Sub
End Class

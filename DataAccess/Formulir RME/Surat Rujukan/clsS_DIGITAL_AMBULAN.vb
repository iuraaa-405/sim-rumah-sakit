Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_AMBULAN
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "RJKAMB"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_AMBULAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_AMBULAN
        End Function
        Public Function GetData(ByVal sKDASESMEN As String) As S_DIGITAL_AMBULAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_AMBULANs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)
        End Function
        Public Function GetDatabyKDREG(ByVal sParameter As String) As List(Of S_DIGITAL_AMBULAN)
            If Not oConnection.GetConnection Then
                GetDatabyKDREG = Nothing
                Exit Function
            End If
            GetDatabyKDREG = oConnection.db.S_DIGITAL_AMBULANs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).OrderBy(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_AMBULAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "INSERT"

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KDASESMEN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)


                Try
                    oConnection.db.S_DIGITAL_AMBULANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_AMBULAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_AMBULANs.FirstOrDefault(Function(x) x.KDASESMEN = entity.KDASESMEN)

                Try
                    oConnection.db.S_DIGITAL_AMBULANs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_AMBULANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function IntegerToRoman(IntNumberValue As Integer) As String
            Dim RomanNumbers As New Dictionary(Of String, Integer)()
            RomanNumbers.Add("M", 1000)
            RomanNumbers.Add("CM", 900)
            RomanNumbers.Add("D", 500)
            RomanNumbers.Add("CD", 400)
            RomanNumbers.Add("C", 100)
            RomanNumbers.Add("XC", 90)
            RomanNumbers.Add("L", 50)
            RomanNumbers.Add("XL", 40)
            RomanNumbers.Add("X", 10)
            RomanNumbers.Add("IX", 9)
            RomanNumbers.Add("V", 5)
            RomanNumbers.Add("IV", 4)
            RomanNumbers.Add("I", 1)

            Dim result As String = ""

            For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
                While IntNumberValue >= pair.Value
                    IntNumberValue -= pair.Value
                    result += pair.Key
                End While
            Next
            Return result
        End Function

        Public Function GetDataDateTimeServer() As DateTime
            Try
                If Not oConnection.GetConnection Then
                    GetDataDateTimeServer = Now
                    Exit Function
                End If

                GetDataDateTimeServer = Now

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)
                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT WAKTU = GETDATE() "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                For xloop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    GetDataDateTimeServer = CDate(ds.Tables("ALL").Rows(xloop)("WAKTU"))
                Next

            Catch ex As Exception
                GetDataDateTimeServer = Now
                Throw ex
            End Try
        End Function

        Public Function SoftDelete(ByVal sKDASESMEN As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    SoftDelete = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_AMBULANs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_AMBULAN", "SOFTDELETE", ex.ToString, sKDASESMEN)
                    Throw ex
                End Try

                SoftDelete = True
            Catch oErr As Exception
                SoftDelete = False
                MsgBox("Gagal Delete Dokumen " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
                Throw oErr
            End Try
        End Function
    End Class
End Namespace
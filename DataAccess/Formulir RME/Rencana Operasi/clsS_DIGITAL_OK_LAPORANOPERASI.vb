Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsS_DIGITAL_OK_LAPORANOPERASI
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sREFERENCE As String = ""

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_LAPORANOPERASI
        End Function
        Public Function GetData() As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.OrderBy(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)
        End Function


        Public Function GetDatabySeq(ByVal sParameter As String, ByVal sSEQ As Integer) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetDatabySeq = Nothing
                Exit Function
            End If
            GetDatabySeq = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter And x.SEQ = sSEQ)
        End Function

        Public Function GetDataDetail(ByVal sParameter As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter).KDPENDAFTARAN
            GetDataDetail = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDKUNJUNGAN = sParameter).ToList()
        End Function
        Public Function GetDataDetailByPendaftaranList(ByVal sParameter As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection() Then
                GetDataDetailByPendaftaranList = Nothing
                Exit Function
            End If
            GetDataDetailByPendaftaranList = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter And x.ISDELETE = 0).ToList()
        End Function
        Public Function GetSequence(ByVal sParameter As String) As Integer
            If Not oConnection.GetConnection() Then
                GetSequence = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter).KDPENDAFTARAN
            GetSequence = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDKUNJUNGAN = sParameter).OrderByDescending(Function(x) x.SEQ).FirstOrDefault.SEQ
        End Function

        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function

        Public Function InsertData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN

                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN And x.SEQ = entity.SEQ)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter


                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDKUNJUNGAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                sREFERENCE = sKDKUNJUNGAN

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDeleteLo(ByVal KDKUNJUNGAN As String, ByVal SEQ As String, ByVal sUSER As String) As Boolean
            'Try
            '    UpdateDeleteLo = False

            '    Dim oConn As New SqlConnection
            '    Dim oComm As New SqlCommand
            '    Dim da As SqlDataAdapter
            '    Dim ds As New DataSet
            '    Dim SQL As String
            '    Dim sConn As String = "Data Source=172.165.115.210;Initial Catalog=DATABASE_MEDREK;Persist Security Info=True;User ID=sa;Password=dust1r@@"
            '    oConn = New SqlConnection(sConn)

            '    If oConn.State = ConnectionState.Closed Then
            '        oConn.Open()
            '    End If

            '    SQL = " UPDATE [dbo].[S_DIGITAL_OK_LAPORANOPERASI]  "
            '    SQL &= "  SET ISDELETE = 1  "
            '    SQL &= "  ,DATEDELETE = '" & Now.ToString("yyyy-M-dd HH:mm:ss") & "'  "
            '    SQL &= "  ,USERDELETE = '" & sUSER & "'  "
            '    SQL &= "WHERE KDKUNJUNGAN = '" & KDKUNJUNGAN & "' AND SEQ = '" & SEQ & "' "

            '    oComm.Connection = oConn
            '    oComm.CommandText = SQL
            '    oComm.CommandTimeout = 120
            '    oComm.CommandType = CommandType.Text

            '    da = New SqlDataAdapter(oComm)
            '    da.Fill(ds, "UPDATELAPORANOPERASI")

            '    UpdateDeleteLo = True
            'Catch oErr As Exception
            '    UpdateDeleteLo = False
            '    MsgBox("Gagal Delete Laporan Operasi " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            'End Try
            ''

            Try
                If Not oConnection.GetConnection Then
                    UpdateDeleteLo = False
                    Exit Function
                End If

                sREFERENCE = KDKUNJUNGAN

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = KDKUNJUNGAN And x.SEQ = SEQ)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDeleteLo = True
            Catch ex As Exception
                UpdateDeleteLo = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace
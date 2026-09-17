Imports System.Threading

Namespace Admission
    Public Class clsPendaftaranPDF
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "PENDAFATRANPDF"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureDetail() As S_PENDAFTARAN_PDF
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_PENDAFTARAN_PDF
        End Function
        Public Function GetStructureDetailList() As List(Of S_PENDAFTARAN_PDF)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_PENDAFTARAN_PDF)
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_PDF)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PDFs.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataListByPendaftaran(ByVal Parameter As String) As List(Of S_PENDAFTARAN_PDF)
            If Not oConnection.GetConnection() Then
                GetDataListByPendaftaran = Nothing
                Exit Function
            End If
            GetDataListByPendaftaran = oConnection.db.S_PENDAFTARAN_PDFs.Where(Function(x) x.KDPENDAFTARAN = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_PDF
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PDFs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataPendaftaranBykdKunjungan(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranBykdKunjungan = Nothing
                Exit Function
            End If
            GetDataPendaftaranBykdKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As List(Of S_PENDAFTARAN_PDF)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.FirstOrDefault.KDPENDAFTARAN
                sSTATUS = "INSERT"
                Dim sKDPENDAFTARAN As String = String.Empty

                Try
                    oConnection.db.S_PENDAFTARAN_PDFs.InsertAllOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As List(Of S_PENDAFTARAN_PDF)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.FirstOrDefault.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_PDFs.Where(Function(x) x.KDPENDAFTARAN = entity.FirstOrDefault.KDPENDAFTARAN)
                Try
                    oConnection.db.S_PENDAFTARAN_PDFs.DeleteAllOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_PDFs.InsertAllOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_PDFs.Where(Function(x) x.KDPENDAFTARAN.Contains(Parameter))

                Try
                    oConnection.db.S_PENDAFTARAN_PDFs.DeleteAllOnSubmit(ds)
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace
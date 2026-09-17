Imports System.Threading

Namespace Digital
    Public Class clsGerdQ
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "GERDQ"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_GERDQ_H
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_GERDQ_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_GERDQ_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_GERDQ_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_GERDQ_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_GERDQ_D)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_GERDQ_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.OrderByDescending(Function(x) x.KDGERDQ).ToList()
        End Function
        Public Function GetDataByRegister(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_GERDQ_H
            If Not oConnection.GetConnectionRME() Then
                GetDataByRegister = Nothing
                Exit Function
            End If
            GetDataByRegister = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetData(ByVal sKDGERDQ As String) As S_DIGITAL_GERDQ_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.FirstOrDefault(Function(x) x.KDGERDQ = sKDGERDQ)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_GERDQ_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_GERDQ_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDGERDQ As String) As List(Of S_DIGITAL_GERDQ_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_GERDQ_Ds.Where(Function(x) x.KDGERDQ = sKDGERDQ).ToList()
        End Function
        'Public Function GetDataKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataKunjungan = Nothing
        '        Exit Function
        '    End If
        '    GetDataKunjungan = oConnection.dbRME.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        'End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_GERDQ_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataPendaftaranList(ByVal sKDPENDAFTARAN As String, ByVal sDATE As DateTime) As List(Of S_DIGITAL_GERDQ_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataPendaftaranList = Nothing
                Exit Function
            End If
            GetDataPendaftaranList = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE)).ToList()
        End Function
        'Public Function GetDataPendaftaranKunjunganList(ByVal sKDKUNJUNGAN As String) As List(Of S_DIGITAL_GERDQ_H)
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataPendaftaranKunjunganList = Nothing
        '        Exit Function
        '    End If
        '    GetDataPendaftaranKunjunganList = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).ToList()
        'End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_GERDQ_H, ByVal entityDetail As List(Of S_DIGITAL_GERDQ_D)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDGERDQ
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    entity.KDGERDQ = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDGERDQ = entity.KDGERDQ
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_GERDQ_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.S_DIGITAL_GERDQ_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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

                InsertData = entity.KDGERDQ
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_GERDQ_H, ByVal entityDetail As List(Of S_DIGITAL_GERDQ_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDGERDQ
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.FirstOrDefault(Function(x) x.KDGERDQ = entity.KDGERDQ)

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.S_DIGITAL_GERDQ_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_GERDQ_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.S_DIGITAL_GERDQ_Ds.Where(Function(x) x.KDGERDQ = entity.KDGERDQ)

                Try
                    oConnection.dbRME.S_DIGITAL_GERDQ_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.S_DIGITAL_GERDQ_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function DeleteData(ByVal sKDGERDQ As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDGERDQ
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.FirstOrDefault(Function(x) x.KDGERDQ = sKDGERDQ)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_GERDQ_Ds.Where(Function(x) x.KDGERDQ = sKDGERDQ)

                Try
                    oConnection.dbRME.S_DIGITAL_GERDQ_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_GERDQ_Ds.DeleteAllOnSubmit(dsDetail)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function UpdateDelete(ByVal sKDGERDQ As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDelete = False
                    Exit Function
                End If

                sREFERENCE = sKDGERDQ

                Try
                    Dim ds = oConnection.dbRME.S_DIGITAL_GERDQ_Hs.FirstOrDefault(Function(x) x.KDGERDQ = sKDGERDQ)

                    ds.ISDELETE = 0
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_GERDQ_H", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDelete = True
            Catch ex As Exception
                UpdateDelete = False
                oError.InsertData("S_DIGITAL_GERDQ_H", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace
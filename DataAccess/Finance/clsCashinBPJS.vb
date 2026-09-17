Imports System.Threading

Namespace Finance
    Public Class clsCashinBPJS
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
            sMODUL = "CSBPJS"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_CASHINBPJS_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_CASHINBPJS_H
        End Function
        Public Function GetStructureDetail() As F_CASHINBPJS_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_CASHINBPJS_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_CASHINBPJS_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_CASHINBPJS_D)
        End Function
        Public Function GetData() As List(Of F_CASHINBPJS_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHINBPJS_Hs.OrderByDescending(Function(x) x.KDCASHINBPJS).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_CASHINBPJS_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHINBPJS_Hs.FirstOrDefault(Function(x) x.KDCASHINBPJS = Parameter)
        End Function
        Public Function GetDataJudul(ByVal KDJUDUL As String) As List(Of F_CASHINBPJS_H)
            If Not oConnection.GetConnection() Then
                GetDataJudul = Nothing
                Exit Function
            End If
            GetDataJudul = oConnection.db.F_CASHINBPJS_Hs.Where(Function(x) x.KDJUDULJASA = KDJUDUL).ToList()
        End Function
        Public Function GetDataSEP(ByVal Parameter As String) As F_CASHINBPJS_D
            If Not oConnection.GetConnection() Then
                GetDataSEP = Nothing
                Exit Function
            End If
            GetDataSEP = oConnection.db.F_CASHINBPJS_Ds.FirstOrDefault(Function(x) x.NOSEP = Parameter)
        End Function
        Public Function GetDataSEP(ByVal Parameter1 As String, ByVal Parameter2 As String) As F_CASHINBPJS_D
            If Not oConnection.GetConnection() Then
                GetDataSEP = Nothing
                Exit Function
            End If
            GetDataSEP = oConnection.db.F_CASHINBPJS_Ds.FirstOrDefault(Function(x) x.NOSEP = Parameter1 And x.KDCASHINBPJS = Parameter2)
        End Function
        Public Function GetDataSEPCek(ByVal Parameter1 As String, ByVal Parameter2 As String) As F_CASHINBPJS_D
            If Not oConnection.GetConnection() Then
                GetDataSEPCek = Nothing
                Exit Function
            End If
            GetDataSEPCek = oConnection.db.F_CASHINBPJS_Ds.FirstOrDefault(Function(x) x.NOSEP = Parameter1 And x.KDCASHINBPJS <> Parameter2)
        End Function
        Public Function GetDataSetting() As SET_SETTING
            If Not oConnection.GetConnection() Then
                GetDataSetting = Nothing
                Exit Function
            End If
            GetDataSetting = oConnection.db.SET_SETTINGs.FirstOrDefault()
        End Function
        Public Function GetDataDetail() As List(Of F_CASHINBPJS_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHINBPJS_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_CASHINBPJS_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHINBPJS_Ds.Where(Function(x) x.KDCASHINBPJS = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As F_CASHINBPJS_H, ByVal entityDetail As List(Of F_CASHINBPJS_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHINBPJS
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

                    entity.KDCASHINBPJS = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDCASHINBPJS = entity.KDCASHINBPJS
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_CASHINBPJS_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_CASHINBPJS_Ds.InsertAllOnSubmit(entityDetail)
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

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
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
        Public Function UpdateData(ByVal entity As F_CASHINBPJS_H, ByVal entityDetail As List(Of F_CASHINBPJS_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHINBPJS
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_CASHINBPJS_Hs.FirstOrDefault(Function(x) x.KDCASHINBPJS = entity.KDCASHINBPJS)

                Try
                    oConnection.db.F_CASHINBPJS_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_CASHINBPJS_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_CASHINBPJS_Ds.Where(Function(x) x.KDCASHINBPJS = entity.KDCASHINBPJS)

                Try
                    oConnection.db.F_CASHINBPJS_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_CASHINBPJS_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.F_CASHINBPJS_Hs.Where(Function(x) x.KDCASHINBPJS.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDCASHINBPJS

                    Dim dsDetail = oConnection.db.F_CASHINBPJS_Ds.Where(Function(x) x.KDCASHINBPJS = sNOCASH)

                    Try
                        oConnection.db.F_CASHINBPJS_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.F_CASHINBPJS_Ds.DeleteAllOnSubmit(dsDetail)
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
                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateNomorSEPKDREG(ByVal sKDCASHINBPJS As String, ByVal sKDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateNomorSEPKDREG = False
                    Exit Function
                End If

                UpdateNomorSEPKDREG = True

                Dim ds = oConnection.db.F_CASHINBPJS_Hs.FirstOrDefault(Function(x) x.KDCASHINBPJS = sKDCASHINBPJS)

                ds.MEMO = "Telah diperbaharui Tanggal : " & Now & " Oleh User : " & sKDUSER

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateNomorSEPKDREG = False
                Throw ex
            End Try
        End Function
        Public Function UpdateNomorSEPKDREG(ByVal sKDCASHINBPJS As String, ByVal sNOMORSEP As String, ByVal sKDPENDAFTARAN As String, ByVal sJASA_PENUNJANG As Decimal, ByVal sJASA_TINDAKANLAIN As Decimal, ByVal sJASA_SISA As Decimal, ByVal sJASA_MEDIS As Decimal, ByVal sJASA_PARAMEDIS As Decimal) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateNomorSEPKDREG = False
                    Exit Function
                End If

                UpdateNomorSEPKDREG = True

                Dim ds = oConnection.db.F_CASHINBPJS_Ds.FirstOrDefault(Function(x) x.KDCASHINBPJS = sKDCASHINBPJS And x.NOSEP = sNOMORSEP)

                ds.KDPENDAFTARAN = sKDPENDAFTARAN
                ds.JASA_PENUNJANG = sJASA_PENUNJANG
                ds.JASA_TINDAKANLAIN = sJASA_TINDAKANLAIN
                ds.JASA_SISA = sJASA_SISA
                ds.JASA_MEDIS = sJASA_MEDIS
                ds.JASA_PARAMEDIS = sJASA_PARAMEDIS

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateNomorSEPKDREG = False
                Throw ex
            End Try
        End Function
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.F_CASHINBPJS_Hs

        '        For Each iLoop In entity
        '            Dim sKDCASHINBPJS = iLoop.KDCASHINBPJS
        '            Dim entityDetail = oConnection.db.F_CASHINBPJS_Ds.Where(Function(x) x.KDCASHINBPJS = sKDCASHINBPJS And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_CASHINBPJS_Ds.Where(Function(x) x.KDCASHINBPJS = sKDCASHINBPJS And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace
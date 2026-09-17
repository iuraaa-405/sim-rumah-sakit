Namespace Reference
    Public Class clsDoctor
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "DOCTOR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DOCTOR
        End Function
        Public Function GetDatakodebpjs(ByVal sKDDEPARTMENT_BPJS As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDatakodebpjs = Nothing
                Exit Function
            End If
            GetDatakodebpjs = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.VCLAIM_KDDPJP = sKDDEPARTMENT_BPJS)
        End Function
        Public Function GetDataByKodeVclaim(ByVal sVCLAIM_KDDPJP As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByKodeVclaim = Nothing
                Exit Function
            End If
            GetDataByKodeVclaim = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.VCLAIM_KDDPJP = sVCLAIM_KDDPJP)
        End Function
        Public Function GetStructureDetail_DOCTOR() As M_DOCTOR_DOCTOR
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DOCTOR = Nothing
            End If
            GetStructureDetail_DOCTOR = New M_DOCTOR_DOCTOR
        End Function
        Public Function GetStructureDetail_DOCTORist() As List(Of M_DOCTOR_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DOCTORist = Nothing
            End If
            GetStructureDetail_DOCTORist = New List(Of M_DOCTOR_DOCTOR)
        End Function
        Public Function GetStructureDetail_DEPARMENT() As M_DOCTOR_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DEPARMENT = Nothing
            End If
            GetStructureDetail_DEPARMENT = New M_DOCTOR_DEPARTMENT
        End Function
        Public Function GetStructureDetail_DEPARTMENTist() As List(Of M_DOCTOR_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DEPARTMENTist = Nothing
            End If
            GetStructureDetail_DEPARTMENTist = New List(Of M_DOCTOR_DEPARTMENT)
        End Function
        Public Function GetStructureDetail_DOCTORBPJS() As M_DOCTOR_BPJ
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DOCTORBPJS = Nothing
            End If
            GetStructureDetail_DOCTORBPJS = New M_DOCTOR_BPJ
        End Function
        Public Function GetStructureDetailList() As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_DOCTOR_JADWAL)
        End Function
        Public Function GetStructureDetail() As M_DOCTOR_JADWAL
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_DOCTOR_JADWAL
        End Function
        Public Function GetStructureDetail_DOCTORBPJSList() As List(Of M_DOCTOR_BPJ)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_DOCTORBPJSList = Nothing
            End If
            GetStructureDetail_DOCTORBPJSList = New List(Of M_DOCTOR_BPJ)
        End Function
        Public Function GetData() As List(Of M_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDDOCTOR As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR)
        End Function
        Public Function GetDataDetail(ByVal sKDDOCTOR As String) As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR).ToList()
        End Function
        Public Function GetDataDetailbyHariDepatment(ByVal sDeparment As String, ByVal sHari As String) As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailbyHariDepatment = Nothing
                Exit Function
            End If
            GetDataDetailbyHariDepatment = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.HARI = sHari And x.M_DOCTOR.M_DEPARTMENT.KDDEPARTMENT = sDeparment And x.LIBUR = False).ToList()
        End Function
        Public Function GetDataByName(ByVal sKDDOCTOR As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.NAME_DISPLAY.Contains(sKDDOCTOR))
        End Function
        Public Function GetDataByStatus(ByVal sISSTATUS As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByStatus = Nothing
                Exit Function
            End If
            GetDataByStatus = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.ISSTATUS = sISSTATUS)
        End Function
        Public Function GetDataDetail_DOCTOR() As List(Of M_DOCTOR_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DOCTOR = Nothing
                Exit Function
            End If
            GetDataDetail_DOCTOR = oConnection.db.M_DOCTOR_DOCTORs.ToList()
        End Function
        Public Function GetDataDetail_DOCTOR(ByVal sKDDOCTOR As String) As List(Of M_DOCTOR_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DOCTOR = Nothing
                Exit Function
            End If
            GetDataDetail_DOCTOR = oConnection.db.M_DOCTOR_DOCTORs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR).ToList()
        End Function
        Public Function GetDataDetail_DEPARMENT() As List(Of M_DOCTOR_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DEPARMENT = Nothing
                Exit Function
            End If
            GetDataDetail_DEPARMENT = oConnection.db.M_DOCTOR_DEPARTMENTs.ToList()
        End Function
        Public Function GetDataDetail_DEPARTMENT(ByVal sKDDOCTOR As String) As List(Of M_DOCTOR_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DEPARTMENT = Nothing
                Exit Function
            End If
            GetDataDetail_DEPARTMENT = oConnection.db.M_DOCTOR_DEPARTMENTs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR).ToList()
        End Function
        Public Function GetDataDetail_DOCTORBPJS() As List(Of M_DOCTOR_BPJ)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DOCTORBPJS = Nothing
                Exit Function
            End If
            GetDataDetail_DOCTORBPJS = oConnection.db.M_DOCTOR_BPJs.ToList()
        End Function
        Public Function GetDataDetail_DOCTORBPJS(ByVal sKDDOCTOR As String) As List(Of M_DOCTOR_BPJ)
            If Not oConnection.GetConnection() Then
                GetDataDetail_DOCTORBPJS = Nothing
                Exit Function
            End If
            GetDataDetail_DOCTORBPJS = oConnection.db.M_DOCTOR_BPJs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR).ToList()
        End Function
        Public Function GetDataDetail_DOCTORBPJSByNAMA(ByVal sMEMO As String) As M_DOCTOR_BPJ
            If Not oConnection.GetConnection() Then
                GetDataDetail_DOCTORBPJSByNAMA = Nothing
                Exit Function
            End If
            GetDataDetail_DOCTORBPJSByNAMA = oConnection.db.M_DOCTOR_BPJs.FirstOrDefault(Function(x) x.MEMO = sMEMO)
        End Function
        Public Function GetDataSync() As List(Of M_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_DOCTORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function GetDataDetailJadwalPoli(ByVal sKDDEPARTMENT As String, ByVal sHARI As String) As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailJadwalPoli = Nothing
                Exit Function
            End If
            GetDataDetailJadwalPoli = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.M_DOCTOR.VCLAIM_KDDPJP <> "" And x.M_DOCTOR.KDDEPARTMENT = sKDDEPARTMENT And x.HARI = sHARI And x.M_DOCTOR.VCLAIM_KDDPJP <> "" And x.LIBUR = False).ToList()
        End Function
        Public Function GetDataDetailJadwalDokter(ByVal sKDDOCTOR As String, ByVal sHARI As String) As M_DOCTOR_JADWAL
            If Not oConnection.GetConnection() Then
                GetDataDetailJadwalDokter = Nothing
                Exit Function
            End If
            GetDataDetailJadwalDokter = oConnection.db.M_DOCTOR_JADWALs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR And x.HARI = sHARI And x.LIBUR = False)
        End Function
        Public Function GetDataMonitoringTotal(ByVal sKODEPOLI As String, ByVal sKODEDOKTER As String, ByVal sDATE As DateTime) As Integer
            If Not oConnection.GetConnection() Then
                GetDataMonitoringTotal = Nothing
                Exit Function
            End If
            GetDataMonitoringTotal = oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.KODEPOLI = sKODEPOLI And x.KODEDOKTER = sKODEDOKTER And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy")).Count()
        End Function
        Public Function GetDataMonitoringSisaJKN(ByVal sKODEPOLI As String, ByVal sKODEDOKTER As String, ByVal sDATE As DateTime) As Integer
            If Not oConnection.GetConnection() Then
                GetDataMonitoringSisaJKN = Nothing
                Exit Function
            End If
            GetDataMonitoringSisaJKN = oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.KODEPOLI = sKODEPOLI And x.KODEDOKTER = sKODEDOKTER And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.JENISPASIEN = "JKN").Count()
        End Function
        Public Function GetDataMonitoringSisaNONJKN(ByVal sKODEPOLI As String, ByVal sKODEDOKTER As String, ByVal sDATE As DateTime) As Integer
            If Not oConnection.GetConnection() Then
                GetDataMonitoringSisaNONJKN = Nothing
                Exit Function
            End If
            GetDataMonitoringSisaNONJKN = oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.KODEPOLI = sKODEPOLI And x.KODEDOKTER = sKODEDOKTER And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.JENISPASIEN <> "JKN").Count()
        End Function
        Public Function GetDataJadwalDokter(ByVal sKDDOCTOR As String, ByVal sHARI As String) As M_DOCTOR_JADWAL
            If Not oConnection.GetConnection() Then
                GetDataJadwalDokter = Nothing
                Exit Function
            End If
            GetDataJadwalDokter = oConnection.db.M_DOCTOR_JADWALs.FirstOrDefault(Function(x) x.M_DOCTOR.KDDOCTOR = sKDDOCTOR And x.HARI = sHARI)
        End Function
        Public Function InsertData(ByVal entity As M_DOCTOR, ByVal KDDOCTOR As String, ByVal entityDetail_Jadwal As List(Of M_DOCTOR_JADWAL), Optional ByVal entityDetail_DOCTOR As List(Of M_DOCTOR_DOCTOR) = Nothing, Optional ByVal entityDetail_DEPARTMENT As List(Of M_DOCTOR_DEPARTMENT) = Nothing, Optional ByVal entityDetail_DOCTORBPJS As List(Of M_DOCTOR_BPJ) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTOR
                sSTATUS = "INSERT"

                If KDDOCTOR = "" Then
                    'Generate Auto Number
                    Try
                        sLASTNUMBER = CInt(oConnection.db.M_DOCTORs.Where(Function(x) x.KDDOCTOR <> "XXX").OrderByDescending(Function(x) x.KDDOCTOR).FirstOrDefault().KDDOCTOR.Remove(0, (sMODUL & " _ ").Length)) + 1
                    Catch ex As Exception
                        sLASTNUMBER = 1
                    End Try
                    'End Generate
                End If

                Try
                    entity.KDDOCTOR = IIf(KDDOCTOR = "", sMODUL & "_" & AutoNumberCode(sLASTNUMBER), KDDOCTOR)
                    oConnection.db.M_DOCTORs.InsertOnSubmit(entity)
                    oConnection.db.M_DOCTOR_JADWALs.InsertAllOnSubmit(entityDetail_Jadwal)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_DOCTOR.Count > 0 Then
                        For Each iLoop In entityDetail_DOCTOR
                            iLoop.KDDOCTOR = IIf(KDDOCTOR = "", sMODUL & "_" & AutoNumberCode(sLASTNUMBER), KDDOCTOR)
                        Next

                        oConnection.db.M_DOCTOR_DOCTORs.InsertAllOnSubmit(entityDetail_DOCTOR)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_DEPARTMENT.Count > 0 Then
                        For Each iLoop In entityDetail_DEPARTMENT
                            iLoop.KDDOCTOR = IIf(KDDOCTOR = "", sMODUL & "_" & AutoNumberCode(sLASTNUMBER), KDDOCTOR)
                        Next

                        oConnection.db.M_DOCTOR_DEPARTMENTs.InsertAllOnSubmit(entityDetail_DEPARTMENT)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_DOCTORBPJS.Count > 0 Then
                        For Each iLoop In entityDetail_DOCTORBPJS
                            iLoop.KDDOCTOR = IIf(KDDOCTOR = "", sMODUL & "_" & AutoNumberCode(sLASTNUMBER), KDDOCTOR)
                        Next

                        oConnection.db.M_DOCTOR_BPJs.InsertAllOnSubmit(entityDetail_DOCTORBPJS)
                    End If
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
        Public Function UpdateData(ByVal entity As M_DOCTOR, ByVal entityDetail_Jadwal As List(Of M_DOCTOR_JADWAL), Optional ByVal entityDetail_DOCTOR As List(Of M_DOCTOR_DOCTOR) = Nothing, Optional ByVal entityDetail_DEPARTMENT As List(Of M_DOCTOR_DEPARTMENT) = Nothing, Optional ByVal entityDetail_DOCTORBPJS As List(Of M_DOCTOR_BPJ) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTOR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = entity.KDDOCTOR)
                Dim dsDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = entity.KDDOCTOR)

                Try
                    oConnection.db.M_DOCTORs.DeleteOnSubmit(ds)
                    oConnection.db.M_DOCTORs.InsertOnSubmit(entity)
                    oConnection.db.M_DOCTOR_JADWALs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.M_DOCTOR_JADWALs.InsertAllOnSubmit(entityDetail_Jadwal)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DOCTOR = oConnection.db.M_DOCTOR_DOCTORs.Where(Function(x) x.KDDOCTOR = entity.KDDOCTOR)

                Try
                    If dsDetail_DOCTOR.Count > 0 Then
                        oConnection.db.M_DOCTOR_DOCTORs.DeleteAllOnSubmit(dsDetail_DOCTOR)
                    End If
                    If entityDetail_DOCTOR.Count > 0 Then
                        oConnection.db.M_DOCTOR_DOCTORs.InsertAllOnSubmit(entityDetail_DOCTOR)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DEPARTMENT = oConnection.db.M_DOCTOR_DEPARTMENTs.Where(Function(x) x.KDDOCTOR = entity.KDDOCTOR)

                Try
                    If dsDetail_DEPARTMENT.Count > 0 Then
                        oConnection.db.M_DOCTOR_DEPARTMENTs.DeleteAllOnSubmit(dsDetail_DEPARTMENT)
                    End If
                    If entityDetail_DEPARTMENT.Count > 0 Then
                        oConnection.db.M_DOCTOR_DEPARTMENTs.InsertAllOnSubmit(entityDetail_DEPARTMENT)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DOCTORBPJS = oConnection.db.M_DOCTOR_BPJs.Where(Function(x) x.KDDOCTOR = entity.KDDOCTOR)

                Try
                    If dsDetail_DOCTORBPJS.Count > 0 Then
                        oConnection.db.M_DOCTOR_BPJs.DeleteAllOnSubmit(dsDetail_DOCTORBPJS)
                    End If
                    If entityDetail_DOCTORBPJS.Count > 0 Then
                        oConnection.db.M_DOCTOR_BPJs.InsertAllOnSubmit(entityDetail_DOCTORBPJS)
                    End If
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
        Public Function DeleteData(ByVal sKDDOCTOR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDOCTOR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR)
                Dim dsDetail_DOCTOR = oConnection.db.M_DOCTOR_DOCTORs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR)
                Dim dsDetail_DEPARTMENT = oConnection.db.M_DOCTOR_DEPARTMENTs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR)
                Dim dsDetail_DOCTORBPJS = oConnection.db.M_DOCTOR_BPJs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR)
                Dim dsDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR)

                Try
                    oConnection.db.M_DOCTORs.DeleteOnSubmit(ds)
                    oConnection.db.M_DOCTOR_DOCTORs.DeleteAllOnSubmit(dsDetail_DOCTOR)
                    oConnection.db.M_DOCTOR_DEPARTMENTs.DeleteAllOnSubmit(dsDetail_DEPARTMENT)
                    oConnection.db.M_DOCTOR_BPJs.DeleteAllOnSubmit(dsDetail_DOCTORBPJS)
                    oConnection.db.M_DOCTOR_JADWALs.DeleteAllOnSubmit(dsDetail)
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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function AccountDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_CUSTOMER
                Try
                    AccountDefault = ds
                Catch ex As Exception
                    AccountDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountDefault = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace
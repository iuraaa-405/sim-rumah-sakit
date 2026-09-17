Imports System.Threading

Namespace EMedrek
    Public Class clsDigital_DischargePlanning
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
            sMODUL = "HNO"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_13
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_13
        End Function
        Public Function GetStructureDetaiDiagnosalList() As List(Of S_DIGITAL_RI_13_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetaiDiagnosalList = Nothing
            End If
            GetStructureDetaiDiagnosalList = New List(Of S_DIGITAL_RI_13_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_RI_13_DIAGNOSA
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_RI_13_DIAGNOSA
        End Function
        'Public Function GetData() As List(Of S_DIGITAL_RI_13)
        '    If Not oConnection.GetConnectionRME() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.dbRME.S_DIGITAL_RI_13s.OrderByDescending(Function(x) x.KDREG).ToList()
        'End Function
        'Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_RI_13)
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataByRM = Nothing
        '        Exit Function
        '    End If
        '    GetDataByRM = oConnection.dbRME.S_DIGITAL_RI_13s.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDREG).ToList()
        'End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_RI_13
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = Parameter And x.ISDELETE = False)
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDREG As String) As List(Of S_DIGITAL_RI_13_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.Where(Function(x) x.KDREG = sKDREG).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Private Function GetDataTotalTransaksi(ByVal kdreg As String) As Integer
            If Not oConnection.GetConnectionRME() Then
                GetDataTotalTransaksi = 0
                Exit Function
            End If
            GetDataTotalTransaksi = oConnection.dbRME.S_DIGITAL_RI_13s.Where(Function(x) x.KDREG.Contains(kdreg)).Count()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_13, ByVal entityDetail As List(Of S_DIGITAL_RI_13_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATE)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDREG = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer

                Try
                    oConnection.dbRME.S_DIGITAL_RI_13s.InsertOnSubmit(entity)
                    oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.InsertAllOnSubmit(entityDetail)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_13, ByVal entityDetail As List(Of S_DIGITAL_RI_13_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.Where(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.dbRME.S_DIGITAL_RI_13s.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_RI_13s.InsertOnSubmit(entity)

                    If dsDetail IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.DeleteAllOnSubmit(dsDetail)
                    End If

                    If entityDetail IsNot Nothing Then
                        oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.InsertAllOnSubmit(entityDetail)
                    End If
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
        Public Function fn_SaveDelete(ByVal Parameter As String) As Boolean
            Try
                Dim dsCek = GetData(Parameter)

                fn_SaveDelete = False

                If dsCek IsNot Nothing Then
                    ' ***** HEADER *****
                    Dim ds = GetStructureHeader()

                    With ds
                        .KDREG = Parameter & "DEL" & GetDataTotalTransaksi(Parameter)
                        .KDCUSTOMER = dsCek.KDCUSTOMER
                        .DATECREATED = dsCek.DATECREATED
                        .DATEUPDATED = Now
                        .DATE = dsCek.DATE
                        .JAM = dsCek.JAM
                        .DOCTOR_KODE = dsCek.DOCTOR_KODE
                        .DOCTOR_NAME_DISPLAY = dsCek.DOCTOR_NAME_DISPLAY
                        .ANAMNESIS_01 = dsCek.ANAMNESIS_01
                        .ANAMNESIS_02 = dsCek.ANAMNESIS_02
                        .ANAMNESIS_03 = dsCek.ANAMNESIS_03
                        .ANAMNESIS_04 = dsCek.ANAMNESIS_04
                        .ANAMNESIS_05 = dsCek.ANAMNESIS_05
                        .ANAMNESIS_06 = dsCek.ANAMNESIS_06
                        .ANAMNESIS_07 = dsCek.ANAMNESIS_07
                        .ANAMNESIS_08 = dsCek.ANAMNESIS_08
                        .ANAMNESIS_09 = dsCek.ANAMNESIS_09
                        .ANAMNESIS_10 = dsCek.ANAMNESIS_10
                        .ANAMNESIS_11 = dsCek.ANAMNESIS_11
                        .ANAMNESIS_12 = dsCek.ANAMNESIS_12
                        .ANAMNESIS_13 = dsCek.ANAMNESIS_13
                        .ANAMNESIS_14 = dsCek.ANAMNESIS_14
                        .ANAMNESIS_15 = dsCek.ANAMNESIS_15
                        .ANAMNESIS_16 = dsCek.ANAMNESIS_16
                        .ANAMNESIS_17 = dsCek.ANAMNESIS_17
                        .ANAMNESIS_18 = dsCek.ANAMNESIS_18
                        .ANAMNESIS_19 = dsCek.ANAMNESIS_19
                        .ANAMNESIS_20 = dsCek.ANAMNESIS_20
                        .ANAMNESIS_21 = dsCek.ANAMNESIS_21
                        .ANAMNESIS_22 = dsCek.ANAMNESIS_22
                        .ANAMNESIS_23 = dsCek.ANAMNESIS_23
                        .ANAMNESIS_24 = dsCek.ANAMNESIS_24
                        .ANAMNESIS_25 = dsCek.ANAMNESIS_25
                        .ANAMNESIS_26 = dsCek.ANAMNESIS_26
                        .ANAMNESIS_27 = dsCek.ANAMNESIS_27
                        .ANAMNESIS_28 = dsCek.ANAMNESIS_28
                        .ANAMNESIS_29 = dsCek.ANAMNESIS_29
                        .ANAMNESIS_30 = dsCek.ANAMNESIS_30
                        .ANAMNESIS_31 = dsCek.ANAMNESIS_31
                        .ANAMNESIS_32 = dsCek.ANAMNESIS_32
                        .ANAMNESIS_33 = dsCek.ANAMNESIS_33
                        .ANAMNESIS_34 = dsCek.ANAMNESIS_34
                        .ANAMNESIS_35 = dsCek.ANAMNESIS_35
                        .ANAMNESIS_36 = dsCek.ANAMNESIS_36
                        .ANAMNESIS_37 = dsCek.ANAMNESIS_37
                        .ANAMNESIS_38 = dsCek.ANAMNESIS_38
                        .CETAK = dsCek.CETAK
                        .KDUSER = dsCek.KDUSER
                        .KDUSER_SIGNATURE = dsCek.KDUSER_SIGNATURE
                        .HARI = dsCek.HARI
                        .ANAMNESIS_15_1_CHEK = dsCek.ANAMNESIS_15_1_CHEK
                        .ANAMNESIS_16_1_CHEK = dsCek.ANAMNESIS_16_1_CHEK
                        .ANAMNESIS_17_1_CHEK = dsCek.ANAMNESIS_17_1_CHEK
                        .ANAMNESIS_18_1_CHEK = dsCek.ANAMNESIS_18_1_CHEK
                        .ANAMNESIS_19_1_CHEK = dsCek.ANAMNESIS_19_1_CHEK
                        .ANAMNESIS_20_1_CHEK = dsCek.ANAMNESIS_20_1_CHEK
                        .ANAMNESIS_15_2_CHEK = dsCek.ANAMNESIS_15_2_CHEK
                        .ANAMNESIS_16_2_CHEK = dsCek.ANAMNESIS_16_2_CHEK
                        .ANAMNESIS_17_2_CHEK = dsCek.ANAMNESIS_17_2_CHEK
                        .ANAMNESIS_18_2_CHEK = dsCek.ANAMNESIS_18_2_CHEK
                        .ANAMNESIS_19_2_CHEK = dsCek.ANAMNESIS_19_2_CHEK
                        .ANAMNESIS_20_2_CHEK = dsCek.ANAMNESIS_20_2_CHEK
                        .ISDELETE = 1
                    End With

                    'DIAGNOSA
                    Dim listdiagnosa As New List(Of String)

                    Dim arrDetailDiagnosa = GetStructureDetaiDiagnosalList()

                    For Each xloop In GetDataDetailDiagnosa(dsCek.KDREG)
                        Dim dsDetail = GetStructureDetailDiagnosa()

                        With dsDetail
                            .SEQ = xloop.SEQ
                            .KDREG = ds.KDREG
                            .KATEGORI = xloop.KATEGORI
                            .NAMADIAGNOSA = xloop.NAMADIAGNOSA
                            .KDDIAGNOSA = xloop.NAMADIAGNOSA
                        End With
                        arrDetailDiagnosa.Add(dsDetail)
                    Next

                    For Each xloop In arrDetailDiagnosa
                        listdiagnosa.Add(xloop.KATEGORI & " " & xloop.KDDIAGNOSA & " " & xloop.NAMADIAGNOSA)
                    Next

                    ds.ANAMNESIS_35 = String.Join(vbCrLf, listdiagnosa.ToArray)
                    fn_SaveDelete = InsertData(ds, arrDetailDiagnosa)
                End If
            Catch oErr As Exception
                Throw oErr
                'MsgBox("Simpan Data Discharge Delete Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
                fn_SaveDelete = False
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String, ByVal sUser As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = Parameter)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.Where(Function(x) x.KDREG = Parameter)

                Try
                    oConnection.dbRME.S_DIGITAL_RI_13s.DeleteOnSubmit(ds)

                    If dsDetail.Count > 0 Then
                        oConnection.dbRME.S_DIGITAL_RI_13_DIAGNOSAs.DeleteAllOnSubmit(dsDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace
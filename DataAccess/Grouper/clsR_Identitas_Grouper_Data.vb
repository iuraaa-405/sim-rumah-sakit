Imports System.Data.SqlClient

Namespace Grouper
    Public Class clsR_Identitas_Grouper_Data
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
            sMODUL = "GROUPER DATA"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER_DATA
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER_DATA
        End Function
        Public Function GetStructureDetaiSpecialCMGlList() As List(Of R_IDENTITAS_GROUPER_DATA_SPECIALCMG)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiSpecialCMGlList = Nothing
            End If
            GetStructureDetaiSpecialCMGlList = New List(Of R_IDENTITAS_GROUPER_DATA_SPECIALCMG)
        End Function
        Public Function GetStructureHeaderRiwayat() As R_IDENTITAS_GROUPER_DATA_RIWAYAT
            If Not oConnection.GetConnection() Then
                GetStructureHeaderRiwayat = Nothing
            End If
            GetStructureHeaderRiwayat = New R_IDENTITAS_GROUPER_DATA_RIWAYAT
        End Function
        Public Function GetStructureDetaiDiagnosalList() As List(Of R_IDENTITAS_GROUPER_DATA_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiDiagnosalList = Nothing
            End If
            GetStructureDetaiDiagnosalList = New List(Of R_IDENTITAS_GROUPER_DATA_DIAGNOSA)
        End Function
        Public Function GetStructureDetaiProsedurlList() As List(Of R_IDENTITAS_GROUPER_DATA_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiProsedurlList = Nothing
            End If
            GetStructureDetaiProsedurlList = New List(Of R_IDENTITAS_GROUPER_DATA_PROSEDUR)
        End Function
        Public Function GetStructureDetaiPersalinanList() As List(Of R_IDENTITAS_GROUPER_DATA_PERSALINAN)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiPersalinanList = Nothing
            End If
            GetStructureDetaiPersalinanList = New List(Of R_IDENTITAS_GROUPER_DATA_PERSALINAN)
        End Function
        Public Function GetStructureDetaiRincianlList() As List(Of R_IDENTITAS_GROUPER_DATA_RINCIAN)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiRincianlList = Nothing
            End If
            GetStructureDetaiRincianlList = New List(Of R_IDENTITAS_GROUPER_DATA_RINCIAN)
        End Function
        Public Function GetStructureDetailDiagnosa() As R_IDENTITAS_GROUPER_DATA_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New R_IDENTITAS_GROUPER_DATA_DIAGNOSA
        End Function
        Public Function GetStructureDetailSpecialCMG() As R_IDENTITAS_GROUPER_DATA_SPECIALCMG
            If Not oConnection.GetConnection() Then
                GetStructureDetailSpecialCMG = Nothing
            End If
            GetStructureDetailSpecialCMG = New R_IDENTITAS_GROUPER_DATA_SPECIALCMG
        End Function
        Public Function GetStructureDetailProsedur() As R_IDENTITAS_GROUPER_DATA_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New R_IDENTITAS_GROUPER_DATA_PROSEDUR
        End Function
        Public Function GetStructureDetailPersalian() As R_IDENTITAS_GROUPER_DATA_PERSALINAN
            If Not oConnection.GetConnection() Then
                GetStructureDetailPersalian = Nothing
            End If
            GetStructureDetailPersalian = New R_IDENTITAS_GROUPER_DATA_PERSALINAN
        End Function
        Public Function GetStructureDetailRincian() As R_IDENTITAS_GROUPER_DATA_RINCIAN
            If Not oConnection.GetConnection() Then
                GetStructureDetailRincian = Nothing
            End If
            GetStructureDetailRincian = New R_IDENTITAS_GROUPER_DATA_RINCIAN
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER_DATA)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_DATAs.OrderByDescending(Function(x) x.kodegrouper).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER_DATA
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_DATAs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
        End Function
        Public Function GetDataByRegisterData(ByVal Parameter As String) As R_IDENTITAS_GROUPER_DATA
            If Not oConnection.GetConnection() Then
                GetDataByRegisterData = Nothing
                Exit Function
            End If
            GetDataByRegisterData = oConnection.db.R_IDENTITAS_GROUPER_DATAs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataByRegister(ByVal Parameter As String) As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetDataByRegister = Nothing
                Exit Function
            End If
            GetDataByRegister = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.norec = Parameter)
        End Function
        Public Function GetDataDiagnosaBySeq(ByVal Parameter As Integer, ByVal seq As Integer) As R_IDENTITAS_GROUPER_DATA_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetDataDiagnosaBySeq = Nothing
                Exit Function
            End If
            GetDataDiagnosaBySeq = oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.FirstOrDefault(Function(x) x.kodegrouper = Parameter And x.seq = seq)
        End Function
        Public Function GetDataProsedurBySeq(ByVal Parameter As Integer, ByVal seq As Integer) As R_IDENTITAS_GROUPER_DATA_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetDataProsedurBySeq = Nothing
                Exit Function
            End If
            GetDataProsedurBySeq = oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.FirstOrDefault(Function(x) x.kodegrouper = Parameter And x.seq = seq)
        End Function
        Public Function GetDataRincianBySeq(ByVal Parameter As Integer, ByVal seq As Integer) As R_IDENTITAS_GROUPER_DATA_RINCIAN
            If Not oConnection.GetConnection() Then
                GetDataRincianBySeq = Nothing
                Exit Function
            End If
            GetDataRincianBySeq = oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.FirstOrDefault(Function(x) x.kodegrouper = Parameter And x.seq = seq)
        End Function
        Public Function GetDataDetailDiagnosa(ByVal Parameter As Integer) As List(Of R_IDENTITAS_GROUPER_DATA_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.Where(Function(x) x.kodegrouper = Parameter).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal Parameter As Integer) As List(Of R_IDENTITAS_GROUPER_DATA_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.Where(Function(x) x.kodegrouper = Parameter).ToList()
        End Function
        Public Function GetDataDetailrincian(ByVal Parameter As Integer) As List(Of R_IDENTITAS_GROUPER_DATA_RINCIAN)
            If Not oConnection.GetConnection() Then
                GetDataDetailrincian = Nothing
                Exit Function
            End If
            GetDataDetailrincian = oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.Where(Function(x) x.kodegrouper = Parameter).ToList()
        End Function
        Public Function GetDataDetailPersalinan(ByVal Parameter As Integer) As List(Of R_IDENTITAS_GROUPER_DATA_PERSALINAN)
            If Not oConnection.GetConnection() Then
                GetDataDetailPersalinan = Nothing
                Exit Function
            End If
            GetDataDetailPersalinan = oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.Where(Function(x) x.kodegrouper = Parameter).ToList()
        End Function
        Public Function GetDataDetailSpecialCMG(ByVal Parameter As Integer, ByVal stype As String) As List(Of R_IDENTITAS_GROUPER_DATA_SPECIALCMG)
            If Not oConnection.GetConnection() Then
                GetDataDetailSpecialCMG = Nothing
                Exit Function
            End If
            GetDataDetailSpecialCMG = oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.Where(Function(x) x.kodegrouper = Parameter And x.type = stype).ToList()
        End Function
        Public Function GetDataDetailSpecialCMGFirst(ByVal Parameter As Integer, ByVal scode As String, ByVal stype As String) As R_IDENTITAS_GROUPER_DATA_SPECIALCMG
            If Not oConnection.GetConnection() Then
                GetDataDetailSpecialCMGFirst = Nothing
                Exit Function
            End If
            GetDataDetailSpecialCMGFirst = oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.FirstOrDefault(Function(x) x.kodegrouper = Parameter And x.code = scode And x.type = stype)
        End Function
        Public Function KodeTarifKlaim_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    KodeTarifKlaim_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KODETARIF_KLAIMs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    KodeTarifKlaim_Default = ds.KODETARIF_KLAIM
                Else
                    KodeTarifKlaim_Default = String.Empty
                End If
            Catch ex As Exception
                KodeTarifKlaim_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function fn_LoadWaktuServer() As DateTime
            Try
                fn_LoadWaktuServer = Now

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)
                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT GETDATE() AS CurrentDateTime "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "WAKTUSERVER")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                For iLoop As Integer = 0 To ds.Tables("WAKTUSERVER").Rows.Count - 1
                    With ds.Tables("WAKTUSERVER")
                        fn_LoadWaktuServer = .Rows(iLoop)("CurrentDateTime")
                    End With
                Next
            Catch oErr As Exception
                fn_LoadWaktuServer = Now
                MsgBox("Eror Ambil Waktu Server" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            End Try
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER_DATA, ByVal entityDiagnosa As List(Of R_IDENTITAS_GROUPER_DATA_DIAGNOSA), ByVal entityProsedur As List(Of R_IDENTITAS_GROUPER_DATA_PROSEDUR), ByVal entityRincian As List(Of R_IDENTITAS_GROUPER_DATA_RINCIAN), ByVal entityPersalinan As List(Of R_IDENTITAS_GROUPER_DATA_PERSALINAN)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATAs.InsertOnSubmit(entity)

                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                    If entityRincian.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.InsertAllOnSubmit(entityRincian)
                    End If
                    If entityPersalinan.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.InsertAllOnSubmit(entityPersalinan)
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

                Try
                    Dim dsRiwayat = GetStructureHeaderRiwayat()
                    With dsRiwayat
                        .datecreated = fn_LoadWaktuServer()
                        .kodegrouper = entity.kodegrouper
                        .seq = 0
                        .memo = entity.MEMO
                        .kduser = entity.NOIDUSER
                    End With

                    InsertDataRiwayat(dsRiwayat)
                Catch ex As Exception

                End Try
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_IDENTITAS_GROUPER_DATA, ByVal entityDiagnosa As List(Of R_IDENTITAS_GROUPER_DATA_DIAGNOSA), ByVal entityProsedur As List(Of R_IDENTITAS_GROUPER_DATA_PROSEDUR), ByVal entityRincian As List(Of R_IDENTITAS_GROUPER_DATA_RINCIAN), ByVal entityPersalinan As List(Of R_IDENTITAS_GROUPER_DATA_PERSALINAN)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATAs.FirstOrDefault(Function(x) x.kodegrouper = entity.kodegrouper)
                Dim dsDiagnosa = oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.Where(Function(x) x.kodegrouper = entity.kodegrouper)
                Dim dsprosedur = oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.Where(Function(x) x.kodegrouper = entity.kodegrouper)
                Dim dsrincian = oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.Where(Function(x) x.kodegrouper = entity.kodegrouper)
                Dim dspersalinan = oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.Where(Function(x) x.kodegrouper = entity.kodegrouper)

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATAs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPER_DATAs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If dsprosedur.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.DeleteAllOnSubmit(dsprosedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                    If dsrincian.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.DeleteAllOnSubmit(dsrincian)
                    End If
                    If entityRincian.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.InsertAllOnSubmit(entityRincian)
                    End If
                    If dspersalinan.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.DeleteAllOnSubmit(dspersalinan)
                    End If
                    If entityPersalinan.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.InsertAllOnSubmit(entityPersalinan)
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

                Try
                    Dim dsRiwayat = GetStructureHeaderRiwayat()
                    With dsRiwayat
                        .datecreated = fn_LoadWaktuServer()
                        .kodegrouper = entity.kodegrouper
                        .seq = 0
                        .memo = entity.MEMO
                        .kduser = entity.NOIDUSER
                    End With

                    InsertDataRiwayat(dsRiwayat)
                Catch ex As Exception

                End Try
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATAs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
                Dim dsDiagnosa = oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.Where(Function(x) x.kodegrouper = Parameter)
                Dim dsprosedur = oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.Where(Function(x) x.kodegrouper = Parameter)
                Dim dsrincian = oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.Where(Function(x) x.kodegrouper = Parameter)
                Dim dspersalinan = oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.Where(Function(x) x.kodegrouper = Parameter)
                Dim dsRiwayat = oConnection.db.R_IDENTITAS_GROUPER_DATA_RIWAYATs.Where(Function(x) x.kodegrouper = Parameter)
                Dim dsSpesialCMG = oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.Where(Function(x) x.kodegrouper = Parameter)


                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATAs.DeleteOnSubmit(ds)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If

                    If dsprosedur.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PROSEDURs.DeleteAllOnSubmit(dsprosedur)
                    End If

                    If dsrincian.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_RINCIANs.DeleteAllOnSubmit(dsrincian)
                    End If

                    If dspersalinan.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_PERSALINANs.DeleteAllOnSubmit(dspersalinan)
                    End If

                    If dsRiwayat.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_RIWAYATs.DeleteAllOnSubmit(dsRiwayat)
                    End If

                    If dsSpesialCMG.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.DeleteAllOnSubmit(dsSpesialCMG)
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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataRiwayat(ByVal entity As R_IDENTITAS_GROUPER_DATA_RIWAYAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataRiwayat = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_RIWAYATs.InsertOnSubmit(entity)
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataRiwayat = True
            Catch ex As Exception
                InsertDataRiwayat = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataSpecialCMG(ByVal entitySpcialCMG As List(Of R_IDENTITAS_GROUPER_DATA_SPECIALCMG)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataSpecialCMG = False
                    Exit Function
                End If

                sREFERENCE = entitySpcialCMG.FirstOrDefault.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.InsertAllOnSubmit(entitySpcialCMG)
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

                InsertDataSpecialCMG = True

            Catch ex As Exception
                InsertDataSpecialCMG = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteDataSpecialCMG(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteDataSpecialCMG = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim dsSpesialCMG = oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.Where(Function(x) x.kodegrouper = Parameter)

                Try
                    If dsSpesialCMG.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.DeleteAllOnSubmit(dsSpesialCMG)

                        Try
                            oConnection.db.SubmitChanges()
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try
                    End If

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteDataSpecialCMG = True
            Catch ex As Exception
                DeleteDataSpecialCMG = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataSpecialCMG(ByVal entitySpcialCMG As List(Of R_IDENTITAS_GROUPER_DATA_SPECIALCMG)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataSpecialCMG = False
                    Exit Function
                End If

                sREFERENCE = entitySpcialCMG.FirstOrDefault.kodegrouper
                sSTATUS = "UPDATE"

                Dim dsSpecialCMG = oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.Where(Function(x) x.kodegrouper = entitySpcialCMG.FirstOrDefault.kodegrouper)

                Try
                    If dsSpecialCMG.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.DeleteAllOnSubmit(dsSpecialCMG)
                    End If
                    If entitySpcialCMG.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_SPECIALCMGs.InsertAllOnSubmit(entitySpcialCMG)
                    End If
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataSpecialCMG = True

            Catch ex As Exception
                UpdateDataSpecialCMG = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataMEMO(ByVal skodegrouper As Integer, ByVal sMEMO As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataMEMO = False
                    Exit Function
                End If

                UpdateDataMEMO = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATAs.FirstOrDefault(Function(x) x.kodegrouper = skodegrouper)

                If ds IsNot Nothing Then
                    ds.MEMO = sMEMO
                    oConnection.db.SubmitChanges()

                    Try
                        Dim dsRiwayat = GetStructureHeaderRiwayat()
                        With dsRiwayat
                            .datecreated = fn_LoadWaktuServer()
                            .kodegrouper = ds.kodegrouper
                            .seq = 0
                            .memo = sMEMO
                            .kduser = ds.NOIDUSER
                        End With

                        InsertDataRiwayat(dsRiwayat)
                    Catch ex As Exception

                    End Try
                End If

            Catch ex As Exception
                UpdateDataMEMO = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace
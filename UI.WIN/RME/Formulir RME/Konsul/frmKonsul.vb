Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmKonsul
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKonsul As New Digital.clsKonsul
    Private sKDKUNJUNGAN As String = String.Empty
    Private sKoneksi As String = String.Empty
    Private sDOCTOR As String = String.Empty
    Private sCATEGORY As Integer = 0
    Private sKDTUJUAN As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
        Dim dsKunjungan = oKonsul.GetDataKunjungan(KDKUNJUNGAN)
        If dsKunjungan IsNot Nothing Then
            sDOCTOR = dsKunjungan.KDDOKTER
            sCATEGORY = dsKunjungan.CATEGORY
            sKDTUJUAN = dsKunjungan.KDTUJUAN
            sKDPENDAFTARAN = dsKunjungan.KDPENDAFTARAN

            If sCATEGORY = 0 Then
                If sKDTUJUAN = "85" Then
                    lRABER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lKONSULSEWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lALIHLEADER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    lRABER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lKONSULSEWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lALIHLEADER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            Else
                lRABER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lKONSULSEWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lALIHLEADER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'deDATE.DateTime = Now

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Konsul.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = sNoId
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        grdKDOCTOR_FROM.Properties.ReadOnly = Status
        grdKDOCTOR_TO.Properties.ReadOnly = Status
        txtKONSUL.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            chkISPERIKSAPOLI.Properties.ReadOnly = False
        Else
            chkISPERIKSAPOLI.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub fn_EmptyMe()
        grdKDOCTOR_FROM.ResetText()
        grdKDOCTOR_TO.ResetText()
        txtKONSUL.ResetText()
        grdKDOCTOR_FROM.Text = sDOCTOR
        'chkISPERIKSAPOLI.Checked = False
        chkISRUBBER.Checked = False
        chkISSEWAKTU.Checked = False
        chkISALIHLEADER.Checked = False

        deDATE.DateTime = Now


        If sCATEGORY = 0 Then
            If sKDTUJUAN = "85" Then
                chkISPERIKSAPOLI.Checked = False
            Else
                chkISPERIKSAPOLI.Checked = True
            End If
        Else
            chkISPERIKSAPOLI.Checked = False
        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKonsul.GetData(sNoId)

            With ds
                grdKDOCTOR_FROM.Text = .KDDOKTER_DARI
                grdKDOCTOR_TO.Text = .KDDOKTER_KEPADA
                txtKONSUL.Text = .MEMO
                chkISPERIKSAPOLI.Checked = .ISPERIKSAPOLI
                chkISRUBBER.Checked = .ISRUBBER
                chkISSEWAKTU.Checked = .ISSEWAKTU
                chkISALIHLEADER.Checked = .ISALIHLEADER

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDOCTOR_FROM.Text = String.Empty Then
                grdKDOCTOR_FROM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDOCTOR_FROM.ErrorText = Statement.ErrorRequired

                grdKDOCTOR_FROM.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDOCTOR_TO.Text = String.Empty Then
                grdKDOCTOR_TO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDOCTOR_TO.ErrorText = Statement.ErrorRequired

                grdKDOCTOR_TO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If sKDKUNJUNGAN = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If
            If sCATEGORY = 1 Then
                Dim chek As Integer = 0
                If chkISSEWAKTU.Checked = True Then
                    chek = 1
                End If
                If chkISALIHLEADER.Checked = True Then
                    chek = 1
                End If
                If chkISRUBBER.Checked = True Then
                    chek = 1
                End If

                If chek = 0 Then
                    MsgBox("SILAHKAN PILIH SALAH SATU KONSUL SEWAKTU ATAU ALIH LEADER ATAU RUBBER", MsgBoxStyle.Critical, Me.Text)

                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oKonsul.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKonsul.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDKONSULTASI = sNoId
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KDDOKTER_DARI = grdKDOCTOR_FROM.EditValue
                .DOKTER_DARI = grdKDOCTOR_FROM.Text
                .KDDOKTER_KEPADA = grdKDOCTOR_TO.EditValue
                .DOKTER_KEPADA = grdKDOCTOR_TO.Text
                .MEMO = txtKONSUL.Text.ToString.Trim.ToUpper
                Try
                    .ISCHEKED = oKonsul.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .ISPERIKSAPOLI = oKonsul.GetData(sNoId).ISPERIKSAPOLI
                Catch ex As Exception
                    .ISPERIKSAPOLI = False
                End Try
                Try
                    .KDKUNJUNGAN_POLI = oKonsul.GetData(sNoId).KDKUNJUNGAN_POLI
                Catch ex As Exception
                    .KDKUNJUNGAN_POLI = " "
                End Try

                .KDUSER = sUserID
                .ISRUBBER = chkISRUBBER.Checked
                .ISSEWAKTU = chkISSEWAKTU.Checked
                .ISALIHLEADER = chkISALIHLEADER.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim sNoKonsul As String = String.Empty
                    If chkISPERIKSAPOLI.Checked = True Then
                        Dim sKODE = fn_SaveKunjunganPoli()
                        If sKODE <> "" Then

                            sNoKonsul = oKonsul.InsertData(ds, sKODE)

                            If sNoKonsul <> "" Then
                                fn_Save = True
                            Else
                                fn_Save = False
                            End If
                        Else
                            fn_Save = False
                        End If
                    Else
                        sNoKonsul = oKonsul.InsertData(ds, "")

                        If sNoKonsul <> "" Then
                            fn_Save = True
                        Else
                            fn_Save = False
                        End If
                    End If

                    sSREQUESTKONSUL_CPPT = sNoKonsul


                    Dim sNowa = GETDATANOWADOCTOR(grdKDOCTOR_TO.EditValue)

                    If sNowa <> "" Then
                        Dim oSetKoneksi As New Brigging.clsSetKoneksi

                        Dim dsSetKoneksi = oSetKoneksi.fn_SentKonsulWA(sNowa, sNoKonsul)
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKonsul.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If chkISALIHLEADER.Checked = True Then
                Dim oBriging As New Brigging.clsSetKoneksi
                If oBriging.UPDATESEP(sKDPENDAFTARAN, grdKDOCTOR_TO.EditValue, Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())) = True Then
                    MsgBox("ALIH LEADER MAKA LEMBAR SEP ATAU REGISTRASI AKAN TERUPDATE KE DOKTER " & grdKDOCTOR_TO.Text & ", SILAHKAN PERBAIKI ADMINISTRASI PENGKLAIMAN", MsgBoxStyle.Critical, Me.Text)
                Else
                    MsgBox("ALIH LEADER MAKA LEMBAR SEP ATAU REGISTRASI TIDAK TERUPDATE KE DOKTER " & grdKDOCTOR_TO.Text & ", SILAHKAN HUBUNGI BAGIAN ADMISI UNTUK PERBAIKAN ADMINISTRASI PENGKLAIMAN", MsgBoxStyle.Critical, Me.Text)
                End If
            End If

            'Dim oResumeRawatJalan As New Digital.clsResumeRawatJalan

            'oResumeRawatJalan.UpdateResumeDataKunjunganRawatJalanNew(sKDKUNJUNGAN, 0)
            'oResumeRawatJalan.UpdateResumeDataKunjunganRawatJalanOld(sKDKUNJUNGAN, 0)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

    Private Function fn_SaveKunjunganPoli() As String
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            Dim sMEMODEPARTMENT As String = GETDATADEPARTMENT(grvKDDOKTERTO.GetFocusedRowCellValue("KDDEPARTMENT"))
            Dim sMEMODEOCTOR As String = GETDATAKDDOCTOR(GETDATAKDDOCTOR(grdKDOCTOR_TO.EditValue))
            Dim kodeantrian As String = IIf(oKonsul.GetDataKunjungan(sKDKUNJUNGAN).PENJAMIN = "BPJS KESEHATAN", "B", "A")

            Dim sMODUL As String = deDATE.DateTime.ToString("dd") & deDATE.DateTime.ToString("MM") & deDATE.DateTime.ToString("yyyy") & "-" & sMEMODEPARTMENT & sMEMODEOCTOR & "P" & kodeantrian
            Dim KDKUNJUNGAN_POLI As String = String.Empty
            Dim sLASTNUMBER As Integer = 0

            SQL = "SELECT * "
            SQL &= "FROM "
            SQL &= "SET_COUNTER "
            SQL &= "WHERE KDCOUNTER = '" & sMODUL & "' "
            SQL &= "AND MONTH = " & CInt(Now.ToString("MM")) & " "
            SQL &= "AND YEAR = " & CInt(Now.ToString("yyyy")) & " "
            SQL &= "AND DAY = " & CInt(Now.ToString("dd")) & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_COUNTER")

            For iLoop As Integer = 0 To ds.Tables("SET_COUNTER").Rows.Count - 1
                With ds.Tables("SET_COUNTER")
                    sLASTNUMBER = .Rows(iLoop)("LASTNUMBER")
                End With
            Next

            If sLASTNUMBER = 0 Then
                SQL = "INSERT INTO "
                SQL &= "SET_COUNTER "
                SQL &= "( "
                SQL &= "KDCOUNTER "
                SQL &= ",LASTNUMBER "
                SQL &= ",MONTH "
                SQL &= ",YEAR "
                SQL &= ",DAY "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & sMODUL & "' "
                SQL &= ",1 "
                SQL &= "," & CInt(Now.ToString("MM")) & " "
                SQL &= "," & CInt(Now.ToString("yyyy")) & " "
                SQL &= "," & CInt(Now.ToString("dd")) & " "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSET_COUNTER")
            Else
                SQL = "UPDATE "
                SQL &= "SET_COUNTER "
                SQL &= "SET LASTNUMBER = " & sLASTNUMBER + 1 & " "
                SQL &= "WHERE "
                SQL &= "KDCOUNTER = '" & sMODUL & "' "
                SQL &= "AND MONTH = " & CInt(Now.ToString("MM")) & " "
                SQL &= "AND YEAR = " & CInt(Now.ToString("yyyy")) & " "
                SQL &= "AND DAY = " & CInt(Now.ToString("dd")) & " "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "UPDATESET_COUNTER")
            End If

            sLASTNUMBER = sLASTNUMBER + 1
            KDKUNJUNGAN_POLI = sMODUL & sLASTNUMBER.ToString.PadLeft(3, "0")

            SQL = "INSERT INTO "
            SQL &= "S_PENDAFTARAN_KUNJUNGANPOLI "
            SQL &= "( "
            SQL &= "DATECREATED "
            SQL &= ",DATEUPDATED "
            SQL &= ",KDKUNJUNGAN_POLI "
            SQL &= ",KDPENDAFTARAN "
            SQL &= ",KDDEPARTMENT "
            SQL &= ",KDDOCTOR "
            SQL &= ",KDPENJAMIN "
            SQL &= ",DATE_MASUK "
            SQL &= ",DATE_KELUAR "
            SQL &= ",ISCHEKED "
            SQL &= ",MEMO "
            SQL &= ",KDUSER "
            SQL &= ",ISJAGA "
            SQL &= ",KDPERUSAHAAN "
            SQL &= ",ISRESUME "
            SQL &= ") "
            SQL &= "VALUES "
            SQL &= "( "
            SQL &= "'" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & KDKUNJUNGAN_POLI & "' "
            SQL &= ",'" & oKonsul.GetDataKunjungan(sKDKUNJUNGAN).KDPENDAFTARAN & "' "
            SQL &= ",'" & grvKDDOKTERTO.GetFocusedRowCellValue("KDDEPARTMENT") & "' "
            SQL &= ",'" & grdKDOCTOR_TO.EditValue & "' "
            SQL &= ",'" & oKonsul.GetDataKunjungan(sKDKUNJUNGAN).KDPENJAMIN & "' "
            SQL &= ",'" & deDATE.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & deDATE.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & 0 & "' "
            SQL &= ",'" & "" & "' "
            SQL &= ",'" & sUserID & "' "
            SQL &= ",'" & 0 & "' "
            SQL &= ",'" & oKonsul.GetDataKunjungan(sKDKUNJUNGAN).KDPERUSAHAAN & "' "
            SQL &= ",'" & 0 & "' "
            SQL &= ") "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERTKUNUJUNGANPOLI")

            fn_SaveKunjunganPoli = KDKUNJUNGAN_POLI

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            fn_SaveKunjunganPoli = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub chkISSEWAKTU_CheckedChanged(sender As Object, e As EventArgs) Handles chkISSEWAKTU.CheckedChanged
        If chkISSEWAKTU.Checked = True Then
            chkISALIHLEADER.Checked = False
            chkISRUBBER.Checked = False
        End If
    End Sub
    Private Sub chkISALIHLEADER_CheckedChanged(sender As Object, e As EventArgs) Handles chkISALIHLEADER.CheckedChanged
        If chkISALIHLEADER.Checked = True Then
            chkISSEWAKTU.Checked = False
            chkISRUBBER.Checked = False
        End If
    End Sub
    Private Sub chkISRUBBER_CheckedChanged(sender As Object, e As EventArgs) Handles chkISRUBBER.CheckedChanged
        If chkISRUBBER.Checked = True Then
            chkISSEWAKTU.Checked = False
            chkISALIHLEADER.Checked = False
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Function GETDATAKDDOCTOR(ByVal KDDOCTOR As String) As String
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.KDDOCTOR = '" & KDDOCTOR & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDOKTER")

            GETDATAKDDOCTOR = ""

            For iLoop As Integer = 0 To ds.Tables("GETDOKTER").Rows.Count - 1
                With ds.Tables("GETDOKTER")
                    GETDATAKDDOCTOR = .Rows(iLoop)("MEMO")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            GETDATAKDDOCTOR = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function GETDATANOWADOCTOR(ByVal KDDOCTOR As String) As String
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.KDDOCTOR = '" & KDDOCTOR & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDOKTER")

            GETDATANOWADOCTOR = ""

            For iLoop As Integer = 0 To ds.Tables("GETDOKTER").Rows.Count - 1
                With ds.Tables("GETDOKTER")
                    GETDATANOWADOCTOR = .Rows(iLoop)("MOBILE")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            GETDATANOWADOCTOR = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function GETDATADEPARTMENT(ByVal KDDEPARMENT As String) As String
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DEPARTMENT A "
            SQL &= "WHERE "
            SQL &= "A.KDDEPARTMENT = '" & KDDEPARMENT & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDEPARTMENT")

            GETDATADEPARTMENT = ""

            For iLoop As Integer = 0 To ds.Tables("GETDEPARTMENT").Rows.Count - 1
                With ds.Tables("GETDEPARTMENT")
                    GETDATADEPARTMENT = .Rows(iLoop)("MEMO")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            GETDATADEPARTMENT = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadKDDOCTOR()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDDOCTOR "
            SQL &= ",A.KDDEPARTMENT "
            SQL &= ",POLI = B.NAME_DISPLAY "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdKDOCTOR_FROM.Properties.DataSource = ds.Tables("DOKTER")
            grdKDOCTOR_FROM.Properties.ValueMember = "KDDOCTOR"
            grdKDOCTOR_FROM.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDOCTOR_TO.Properties.DataSource = ds.Tables("DOKTER")
            grdKDOCTOR_TO.Properties.ValueMember = "KDDOCTOR"
            grdKDOCTOR_TO.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class
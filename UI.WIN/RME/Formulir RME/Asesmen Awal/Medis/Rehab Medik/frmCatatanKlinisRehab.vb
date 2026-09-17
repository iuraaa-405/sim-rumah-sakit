Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmCatatanKlinisRehab
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oRehabMedik As New EMedrek.clsFisioterafi_2
    Private sKDDOCTOR As String = String.Empty
    Private sTanggal As DateTime = Now
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KodeKunjungan As String)
        oFormMode = FormMode
        txtKDKUNJUNGAN.Text = KodeKunjungan

        Dim dsKunjungan = oRehabMedik.GetDatabykodeKunjungan(txtKDKUNJUNGAN.Text)
        If dsKunjungan IsNot Nothing Then
            txtKDCUSTOMER.Text = dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
            txtNAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
            txtTANGGALLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
            txtTANGGALDAFTAR.Text = dsKunjungan.DATE.ToString("dd-MM-yyyy")
            sKDDOCTOR = dsKunjungan.KDDOCTOR
            txtTUJUAN.Text = dsKunjungan.M_DEPARTMENT.NAME_DISPLAY

            sTanggal = dsKunjungan.DATE
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "FORMULIR CATATAN KLINIS REHABILITASI MEDIK"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDPJP()

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
        'btnSaveClosee.Enabled = False
        'btnSaveClose.Enabled = Not Status
    End Sub
    Private Sub fn_EmptyMe()
        grdKDDOCTOR.Text = sKDDOCTOR
        deDATE.DateTime = sTanggal
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oRehabMedik.GetData(txtKDKUNJUNGAN.Text)
            With ds
                deDATE.DateTime = .DATE
                txtTUJUAN.Text = .TUJUAN
                grdKDDOCTOR.Text = .KDDOCTOR
                txtDIAGNOSA.Text = .DIAGNOSA
                txtASESMENMINGGU1.Text = .ASESMENMINGGU1
                txtASESMENMINGGU1_CEKLIS.Text = .ASESMENMINGGU1_CEKLIS
                txtGEJALAMINGGU1.Text = .GEJALAMINGGU1
                txtSTATUSGANGGUANMINGGU1.Text = .STATUSGANGGUANMINGGU1
                chkPROGRAMTERAPI_1.Checked = .PROGRAMTERAPI_1
                chkPROGRAMTERAPI_2.Checked = .PROGRAMTERAPI_2
                chkPROGRAMTERAPI_3.Checked = .PROGRAMTERAPI_3
                txtPARAMETER.Text = .PARAMETER
                txtEDUKASI.Text = .EDUKASI
                txtREASESMENMINGGU2.Text = .REASESMENSETELAHMINGGU2
                txtREASESMENMINGGU2_CEKLIS.Text = .REASESMENSETELAHMINGGU2_CEKLIS
                chkPROGRAMTERAPIMINGGU2_1.Checked = .PROGRAMTERAPIMINGGU2_1
                chkPROGRAMTERAPIMINGGU2_2.Checked = .PROGRAMTERAPIMINGGU2_2
                chkREKOMENDASI_1.Checked = .REKOMENDASI_1
                txtREASESMENSETELAHMINGGU4.Text = .REASESMENSETELAHMINGGU4
                txtREASESMENSETELAHMINGGU4_CEKLIS.Text = .REASESMENSETELAHMINGGU4_CEKLIS
                txtGEJALAMINGGU4.Text = .GEJALAMINGGU4
                txtSTATUSGANGGUANMINGGU4.Text = .STATUSGANGGUANMINGGU4
                chkPARAMTERMINGGU4_1.Checked = .PARAMTERMINGGU4_1
                chkPARAMTERMINGGU4_2.Checked = .PARAMTERMINGGU4_2
                txtHAMBATANMINGGU4.Text = .HAMBATANMINGGU4
                chkREKOMENDASITINDAKLANJUT_1.Checked = .REKOMENDASITINDAKLANJUT_1
                chkREKOMENDASITINDAKLANJUT_2.Checked = .REKOMENDASITINDAKLANJUT_2
                chkREKOMENDASITINDAKLANJUT_3.Checked = .REKOMENDASITINDAKLANJUT_3

                txtREASESMENMINGGU4.Text = .REASESMENSETELAHMINGGU4_NEW
                txtREASESMENMINGGU4_CEKLIS.Text = .REASESMENSETELAHMINGGU4_CEKLIS_NEW
                chkPROGRAMTERAPIMINGGU4_1.Checked = .PROGRAMTERAPIMINGGU4_1_NEW
                chkPROGRAMTERAPIMINGGU4_2.Checked = .PROGRAMTERAPIMINGGU4_2_NEW
                chkREKOMENDASI4_1.Checked = .PROGRAMTERAPIMINGGU4_3_NEW
            End With
        Catch oErr As Exception
            MsgBox("Load Data: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDKUNJUNGAN.Text = String.Empty Then
                txtKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTUJUAN.Text = String.Empty Then
                txtTUJUAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTUJUAN.ErrorText = Statement.ErrorRequired

                txtTUJUAN.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oRehabMedik.GetStructureHeader
            With ds
                .DATE = deDATE.DateTime
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .TUJUAN = txtTUJUAN.Text
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .DIAGNOSA = txtDIAGNOSA.Text
                .ASESMENMINGGU1 = txtASESMENMINGGU1.Text
                .ASESMENMINGGU1_CEKLIS = txtASESMENMINGGU1_CEKLIS.Text
                .GEJALAMINGGU1 = txtGEJALAMINGGU1.Text
                .STATUSGANGGUANMINGGU1 = txtSTATUSGANGGUANMINGGU1.Text
                .PROGRAMTERAPI_TEXT = ""
                .PROGRAMTERAPI_1 = chkPROGRAMTERAPI_1.Checked
                .PROGRAMTERAPI_2 = chkPROGRAMTERAPI_2.Checked
                .PROGRAMTERAPI_3 = chkPROGRAMTERAPI_3.Checked
                .PARAMETER = txtPARAMETER.Text
                .EDUKASI = txtEDUKASI.Text
                .REASESMENSETELAHMINGGU2 = txtREASESMENMINGGU2.Text
                .REASESMENSETELAHMINGGU2_CEKLIS = txtREASESMENMINGGU2_CEKLIS.Text
                .PROGRAMTERAPIMINGGU2_TEXT = ""
                .PROGRAMTERAPIMINGGU2_1 = chkPROGRAMTERAPIMINGGU2_1.Checked
                .PROGRAMTERAPIMINGGU2_2 = chkPROGRAMTERAPIMINGGU2_2.Checked
                .REKOMENDASI_TEXT = ""
                .REKOMENDASI_1 = chkREKOMENDASI_1.Checked
                .REASESMENSETELAHMINGGU4 = txtREASESMENSETELAHMINGGU4.Text
                .REASESMENSETELAHMINGGU4_CEKLIS = txtREASESMENSETELAHMINGGU4_CEKLIS.Text
                .GEJALAMINGGU4 = txtGEJALAMINGGU4.Text
                .STATUSGANGGUANMINGGU4 = txtSTATUSGANGGUANMINGGU4.Text
                .PARAMTERMINGGU4_TEXT = ""
                .PARAMTERMINGGU4_1 = chkPARAMTERMINGGU4_1.Checked
                .PARAMTERMINGGU4_2 = chkPARAMTERMINGGU4_2.Checked
                .HAMBATANMINGGU4 = txtHAMBATANMINGGU4.Text
                .REKOMENDASITINDAKLANJUT = ""
                .REKOMENDASITINDAKLANJUT_1 = chkREKOMENDASITINDAKLANJUT_1.Checked
                .REKOMENDASITINDAKLANJUT_2 = chkREKOMENDASITINDAKLANJUT_2.Checked
                .REKOMENDASITINDAKLANJUT_3 = chkREKOMENDASITINDAKLANJUT_3.Checked
                .KDUSER = sUserID
                .ISDELETE = False
                .USERDELETE = ""

                .REASESMENSETELAHMINGGU4_NEW = txtREASESMENMINGGU4.Text
                .REASESMENSETELAHMINGGU4_CEKLIS_NEW = txtREASESMENMINGGU4_CEKLIS.Text
                .PROGRAMTERAPIMINGGU4_1_NEW = chkPROGRAMTERAPIMINGGU4_1.Checked
                .PROGRAMTERAPIMINGGU4_2_NEW = chkPROGRAMTERAPIMINGGU4_2.Checked
                .PROGRAMTERAPIMINGGU4_3_NEW = chkREKOMENDASI4_1.Checked

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oRehabMedik.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_Save = oRehabMedik.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Command Button"
    Private Sub txtASESMENMINGGU1_CEKLIS_Click(sender As Object, e As EventArgs) Handles txtASESMENMINGGU1_CEKLIS.Click
        If txtASESMENMINGGU1_CEKLIS.Text = "" Then
            txtASESMENMINGGU1_CEKLIS.Text = "X"
        Else
            txtASESMENMINGGU1_CEKLIS.ResetText()
        End If
    End Sub
    Private Sub txtREASESMENMINGGU2_CEKLIS_Click(sender As Object, e As EventArgs) Handles txtREASESMENMINGGU2_CEKLIS.Click
        If txtREASESMENMINGGU2_CEKLIS.Text = "" Then
            txtREASESMENMINGGU2_CEKLIS.Text = "X"
        Else
            txtREASESMENMINGGU2_CEKLIS.ResetText()
        End If
    End Sub
    Private Sub txtREASESMENMINGGU4_CEKLIS_Click(sender As Object, e As EventArgs) Handles txtREASESMENMINGGU4_CEKLIS.Click
        If txtREASESMENMINGGU4_CEKLIS.Text = "" Then
            txtREASESMENMINGGU4_CEKLIS.Text = "X"
        Else
            txtREASESMENMINGGU4_CEKLIS.ResetText()
        End If
    End Sub
    Private Sub txtREASESMENSETELAHMINGGU4_CEKLIS_Click(sender As Object, e As EventArgs) Handles txtREASESMENSETELAHMINGGU4_CEKLIS.Click
        If txtREASESMENSETELAHMINGGU4_CEKLIS.Text = "" Then
            txtREASESMENSETELAHMINGGU4_CEKLIS.Text = "X"
        Else
            txtREASESMENSETELAHMINGGU4_CEKLIS.ResetText()
        End If
    End Sub
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub btnResume_Click() Handles btnResume.ItemClick
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            MsgBox("Catatan Klinis Belum di Simpan", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim oRehab1 As New EMedrek.clsFisioterafi_1
            Dim oRehab As New EMedrek.clsFisioterafi_3
            Dim dsRehab1 = oRehab1.GetData(txtKDKUNJUNGAN.Text)
            If dsRehab1 IsNot Nothing Then
                Dim ds = oRehab.GetData(txtKDKUNJUNGAN.Text)
                If ds IsNot Nothing Then
                    Dim frmResumeRawatJalanRehab As New frmResumeRawatJalanRehab
                    Try
                        frmResumeRawatJalanRehab.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDKUNJUNGAN, txtDIAGNOSA.Text, grdKDDOCTOR.EditValue, dsRehab1.ALAMATSIMPANTTDPASIEN)
                        frmResumeRawatJalanRehab.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Dim frmResumeRawatJalanRehab As New frmResumeRawatJalanRehab
                    Try
                        frmResumeRawatJalanRehab.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDKUNJUNGAN.Text, txtDIAGNOSA.Text, grdKDDOCTOR.EditValue, dsRehab1.ALAMATSIMPANTTDPASIEN)
                        frmResumeRawatJalanRehab.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Else
                MsgBox("Lembar Uji Fungsi Belum di Simpan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            Trace.WriteLine("Scrolled up!")
            fn_ScrollPage(True)
        Else
            Trace.WriteLine("Scrolled down!")
            fn_ScrollPage(False)
        End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel2.AutoScrollPosition

        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.Y = -scrollchange - myView.Y

        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y

        End If

        Me.Panel2.AutoScrollPosition = myView
    End Sub
#End Region
End Class
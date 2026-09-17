Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekAsmedKebidanan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASMEDOBGYN As New Digital.clsS_DIGITAL_ASMEDOBGYN
    Private sIsOtority As Boolean = False
    Private sDokter As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNOREG.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNAMAISTRI.Text = dsPendaftaran.NAMAPASIEN
            txtTTLISTRI.Text = dsPendaftaran.TEMPATLAHIR.Trim &" "& dsPendaftaran.TANGGALLAHIR
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtNOREG.Text = dsPendaftaran.KDKUNJUNGAN
            txtKELAS.Text = dsPendaftaran.KELASPELAYANAN
            txtJAMAWALASESMESMEN.Text = Now
            txtTANGGAL.Text = Now
            sDokter = dsPendaftaran.DOKTER
        Else
            txtNAMAISTRI.ResetText()
            txtTTLISTRI.ResetText()
            txtNOMORRM.ResetText()
            txtNOREG.ResetText()
            txtKELAS.ResetText()
            txtJAMAWALASESMESMEN.ResetText()
            txtTANGGAL.ResetText()

        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS OBSTETRI & GYNEKOLOGI"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()

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
        txtNAMASUAMI.Properties.ReadOnly = Status
        txtTTLSUAMI.Properties.ReadOnly = Status
        txtJAMAWALASESMESMEN.Properties.ReadOnly = Status
        txtTANGGAL.Properties.ReadOnly = Status

        txtANAMNESIS.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status
        txtRIWAYATPENYAKITSEKARANG.Properties.ReadOnly = Status
        txtRIWAYATGINEKOLOGI.Properties.ReadOnly = Status
        txtRIWAYATLAIN.Properties.ReadOnly = Status
        chkPERNAHDIRAWATYA.Properties.ReadOnly = Status
        chkPERNAHDIRAWATTIDAK.Properties.ReadOnly = Status
        txtPERNAHDIRAWATKAPAN.Properties.ReadOnly = Status
        txtPERNAHDIRAWATDIMANA.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtKESADARAN.Properties.ReadOnly = Status
        txtBB.Properties.ReadOnly = Status
        txtTB.Properties.ReadOnly = Status
        txtGIZI.Properties.ReadOnly = Status
        txtTENSI.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        txtRESPIRASI.Properties.ReadOnly = Status
        txtGOLDAR.Properties.ReadOnly = Status
        txtALERGI.Properties.ReadOnly = Status

        txtKEPALA1.Properties.ReadOnly = Status
        txtKEPALA2.Properties.ReadOnly = Status
        txtKEPALA3.Properties.ReadOnly = Status
        txtKEPALA4.Properties.ReadOnly = Status
        txtBUAHDADA1.Properties.ReadOnly = Status
        txtBUAHDADA2.Properties.ReadOnly = Status
        txtBUAHDADA3.Properties.ReadOnly = Status
        txtBUAHDADA4.Properties.ReadOnly = Status
        txtPERUT1.Properties.ReadOnly = Status
        txtPERUT2.Properties.ReadOnly = Status
        txtPERUT3.Properties.ReadOnly = Status
        txtPERUT4.Properties.ReadOnly = Status
        txtPERUT5.Properties.ReadOnly = Status
        txtPERUT6.Properties.ReadOnly = Status
        txtPERIKSADALAM1.Properties.ReadOnly = Status
        txtPERIKSADALAM2.Properties.ReadOnly = Status
        txtPERIKSADALAM3.Properties.ReadOnly = Status
        txtPERIKSADALAM4.Properties.ReadOnly = Status
        txtPERIKSADALAM5.Properties.ReadOnly = Status
        txtLUKA.Properties.ReadOnly = Status

        txtKETERANGANPIC.Properties.ReadOnly = Status
        txtGINEKOLOGI1.Properties.ReadOnly = Status
        txtGINEKOLOGI2.Properties.ReadOnly = Status
        txtGINEKOLOGI3.Properties.ReadOnly = Status
        txtGINEKOLOGI4.Properties.ReadOnly = Status
        txtGINEKOLOGI5.Properties.ReadOnly = Status
        txtGINEKOLOGI6.Properties.ReadOnly = Status
        txtGINEKOLOGI7.Properties.ReadOnly = Status
        txtPALPASI1.Properties.ReadOnly = Status
        txtPALPASI2.Properties.ReadOnly = Status
        txtPALPASI3.Properties.ReadOnly = Status
        txtPALPASI4.Properties.ReadOnly = Status
        txtPALPASI5.Properties.ReadOnly = Status
        txtPALPASI6.Properties.ReadOnly = Status
        txtUSG.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG.Properties.ReadOnly = Status
        txtDIFERENSIALDIAGNOSIS.Properties.ReadOnly = Status
        txtDIAGNOSISKERJA.Properties.ReadOnly = Status
        txtPENGOBATANDANTINDAKAN.Properties.ReadOnly = Status
        txtREKONSILIASIOBAT.Properties.ReadOnly = Status
        txtDISCHARGEPLANNING.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                sIsOtority = True
            Else
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        txtNAMASUAMI.ResetText()
        txtTTLSUAMI.ResetText()

        txtANAMNESIS.ResetText()
        txtKELUHANUTAMA.ResetText()
        txtRIWAYATPENYAKITSEKARANG.ResetText()
        txtRIWAYATGINEKOLOGI.ResetText()
        txtRIWAYATLAIN.ResetText()
        chkPERNAHDIRAWATYA.Checked = False
        chkPERNAHDIRAWATTIDAK.Checked = False
        txtPERNAHDIRAWATKAPAN.ResetText()
        txtPERNAHDIRAWATDIMANA.ResetText()
        txtKEADAANUMUM.ResetText()
        txtKESADARAN.ResetText()
        txtBB.ResetText()
        txtTB.ResetText()
        txtGIZI.ResetText()
        txtTENSI.ResetText()
        txtNADI.ResetText()
        txtSUHU.ResetText()
        txtRESPIRASI.ResetText()
        txtGOLDAR.ResetText()
        txtALERGI.ResetText()

        txtKEPALA1.ResetText()
        txtKEPALA2.ResetText()
        txtKEPALA3.ResetText()
        txtKEPALA4.ResetText()
        txtBUAHDADA1.ResetText()
        txtBUAHDADA2.ResetText()
        txtBUAHDADA3.ResetText()
        txtBUAHDADA4.ResetText()
        txtPERUT1.ResetText()
        txtPERUT2.ResetText()
        txtPERUT3.ResetText()
        txtPERUT4.ResetText()
        txtPERUT5.ResetText()
        txtPERUT6.ResetText()
        txtPERIKSADALAM1.ResetText()
        txtPERIKSADALAM2.ResetText()
        txtPERIKSADALAM3.ResetText()
        txtPERIKSADALAM4.ResetText()
        txtPERIKSADALAM5.ResetText()
        txtLUKA.ResetText()
        'picPERUT
        txtKETERANGANPIC.ResetText()
        txtGINEKOLOGI1.ResetText()
        txtGINEKOLOGI2.ResetText()
        txtGINEKOLOGI3.ResetText()
        txtGINEKOLOGI4.ResetText()
        txtGINEKOLOGI5.ResetText()
        txtGINEKOLOGI6.ResetText()
        txtGINEKOLOGI7.ResetText()
        txtPALPASI1.ResetText()
        txtPALPASI2.ResetText()
        txtPALPASI3.ResetText()
        txtPALPASI4.ResetText()
        txtPALPASI5.ResetText()
        txtPALPASI6.ResetText()
        txtUSG.ResetText()
        txtPEMERIKSAANPENUNJANG.ResetText()
        txtDIFERENSIALDIAGNOSIS.ResetText()
        txtDIAGNOSISKERJA.ResetText()
        txtPENGOBATANDANTINDAKAN.ResetText()
        txtREKONSILIASIOBAT.ResetText()
        txtDISCHARGEPLANNING.ResetText()

        deDATE.DateTime = Now

        fn_LoadAsessmenKeperawatan(txtNOREG.Text)

    End Sub
    Private Sub fn_LoadAsessmenKeperawatan(ByVal Parameter As String)
        Dim oAsessmenKeperawatan As New Digital.clsDigital_RJ_23
        Dim dsAsessmenKeperawatan = oAsessmenKeperawatan.GetData(Parameter)

        If dsAsessmenKeperawatan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            txtKELUHANUTAMA.Text = dsAsessmenKeperawatan.SDIGITALRJ23_4
            txtKEADAANUMUM.Text = dsAsessmenKeperawatan.SDIGITALRJ23_215
            txtNADI.Text = dsAsessmenKeperawatan.SDIGITALRJ23_221
            txtTENSI.Text = dsAsessmenKeperawatan.SDIGITALRJ23_220
            txtSUHU.Text = dsAsessmenKeperawatan.SDIGITALRJ23_315
            txtRESPIRASI.Text = dsAsessmenKeperawatan.SDIGITALRJ23_222

        Else
            'MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text)

            With ds
                txtNAMASUAMI.Text = .NAMASUAMI
                txtTTLSUAMI.Text = .TTLSUAMI
                txtJAMAWALASESMESMEN.EditValue = .JAMAWALASESMEN
                txtTANGGAL.EditValue = .DATE

                txtANAMNESIS.Text = .ANAMNESIS
                txtKELUHANUTAMA.Text = .KELUHANUTAMA
                txtRIWAYATPENYAKITSEKARANG.Text = .RIWAYATPENYAKITSEKARANG
                txtRIWAYATGINEKOLOGI.Text = .RIWAYATGINEKOLOGI
                txtRIWAYATLAIN.Text = .RIWAYATLAIN
                chkPERNAHDIRAWATYA.Checked = .PERNAHDIRAWATYA
                chkPERNAHDIRAWATTIDAK.Checked = .PERNAHDIRAWATTIDAK
                txtPERNAHDIRAWATKAPAN.Text = .PERNAHDIRAWATKAPAN
                txtPERNAHDIRAWATDIMANA.Text = .PERNAHDIRAWATDIMANA
                txtKEADAANUMUM.Text = .KEADAANUMUM
                txtKESADARAN.Text = .KESADARAN
                txtBB.Text = .BB
                txtTB.Text = .TB
                txtGIZI.Text = .GIZI
                txtTENSI.Text = .TENSI
                txtNADI.Text = .NADI
                txtSUHU.Text = .SUHU
                txtRESPIRASI.Text = .RESPIRASI
                txtGOLDAR.Text = .GOLDAR
                txtALERGI.Text = .ALERGI

                txtKEPALA1.Text = .KEPALA1
                txtKEPALA2.Text = .KEPALA2
                txtKEPALA3.Text = .KEPALA3
                txtKEPALA4.Text = .KEPALA4
                txtBUAHDADA1.Text = .BUAHDADA1
                txtBUAHDADA2.Text = .BUAHDADA2
                txtBUAHDADA3.Text = .BUAHDADA3
                txtBUAHDADA4.Text = .BUAHDADA4
                txtPERUT1.Text = .PERUT1
                txtPERUT2.Text = .PERUT2
                txtPERUT3.Text = .PERUT3
                txtPERUT4.Text = .PERUT4
                txtPERUT5.Text = .PERUT5
                txtPERUT6.Text = .PERUT6
                txtPERIKSADALAM1.Text = .PERIKSADALAM1
                txtPERIKSADALAM2.Text = .PERIKSADALAM2
                txtPERIKSADALAM3.Text = .PERIKSADALAM3
                txtPERIKSADALAM4.Text = .PERIKSADALAM4
                txtPERIKSADALAM5.Text = .PERIKSADALAM5
                txtLUKA.Text = .LUKA
                
                Try
                    Dim img = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).picPERUT

                    picPERUT.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                     MsgBox("Load Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try

                txtKETERANGANPIC.Text = .KETERANGANPIC
                txtGINEKOLOGI1.Text = .GINEKOLOGI1
                txtGINEKOLOGI2.Text = .GINEKOLOGI2
                txtGINEKOLOGI3.Text = .GINEKOLOGI3
                txtGINEKOLOGI4.Text = .GINEKOLOGI4
                txtGINEKOLOGI5.Text = .GINEKOLOGI5
                txtGINEKOLOGI6.Text = .GINEKOLOGI6
                txtGINEKOLOGI7.Text = .GINEKOLOGI7
                txtPALPASI1.Text = .PALPASI1
                txtPALPASI2.Text = .PALPASI2
                txtPALPASI3.Text = .PALPASI3
                txtPALPASI4.Text = .PALPASI4
                txtPALPASI5.Text = .PALPASI5
                txtPALPASI6.Text = .PALPASI6
                txtUSG.Text = .USG
                txtPEMERIKSAANPENUNJANG.Text = .PEMERIKSAANPENUNJANG
                txtDIFERENSIALDIAGNOSIS.Text = .DIFERENSIALDIAGNOSIS
                txtDIAGNOSISKERJA.Text = .DIAGNOSISKERJA
                txtPENGOBATANDANTINDAKAN.Text = .PENGOBATANDANTINDAKAN
                txtREKONSILIASIOBAT.Text = .REKONSILIASIOBAT
                txtDISCHARGEPLANNING.Text = .DISCHARGEPLANNING

                deDATE.DateTime = .DATE

                BindingSource1.DataSource = os_DIGITAL_ASMEDOBGYN.GetDataDetail(txtNOREG.Text)
                grdRiwayatObstetri.DataSource = BindingSource1

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASMEDOBGYN.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).DATECREATED
                    .WAKTUSELESAIASESMEN = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).WAKTUSELESAIASESMEN
                Catch ex As Exception
                    .DATECREATED = Now
                    .WAKTUSELESAIASESMEN = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime

                .NAMASUAMI = txtNAMASUAMI.Text
                .TTLSUAMI = txtTTLSUAMI.Text
                .JAMAWALASESMEN = txtJAMAWALASESMESMEN.EditValue
                '.DATE = txtTANGGAL.EditValue

                .ANAMNESIS = txtANAMNESIS.Text
                .KELUHANUTAMA = txtKELUHANUTAMA.Text
                .RIWAYATPENYAKITSEKARANG = txtRIWAYATPENYAKITSEKARANG.Text
                .RIWAYATGINEKOLOGI = txtRIWAYATGINEKOLOGI.Text
                .RIWAYATLAIN = txtRIWAYATLAIN.Text
                .PERNAHDIRAWATYA = chkPERNAHDIRAWATYA.Checked
                .PERNAHDIRAWATTIDAK = chkPERNAHDIRAWATTIDAK.Checked
                .PERNAHDIRAWATKAPAN = txtPERNAHDIRAWATKAPAN.Text
                .PERNAHDIRAWATDIMANA = txtPERNAHDIRAWATDIMANA.Text
                .KEADAANUMUM = txtKEADAANUMUM.Text
                .KESADARAN = txtKESADARAN.Text
                .BB = txtBB.Text
                .TB = txtTB.Text
                .GIZI = txtGIZI.Text
                .TENSI = txtTENSI.Text
                .NADI = txtNADI.Text
                .SUHU = txtSUHU.Text
                .RESPIRASI = txtRESPIRASI.Text
                .GOLDAR = txtGOLDAR.Text
                .ALERGI = txtALERGI.Text

                .KEPALA1 = txtKEPALA1.Text
                .KEPALA2 = txtKEPALA2.Text
                .KEPALA3 = txtKEPALA3.Text
                .KEPALA4 = txtKEPALA4.Text
                .BUAHDADA1 = txtBUAHDADA1.Text
                .BUAHDADA2 = txtBUAHDADA2.Text
                .BUAHDADA3 = txtBUAHDADA3.Text
                .BUAHDADA4 = txtBUAHDADA4.Text
                .PERUT1 = txtPERUT1.Text
                .PERUT2 = txtPERUT2.Text
                .PERUT3 = txtPERUT3.Text
                .PERUT4 = txtPERUT4.Text
                .PERUT5 = txtPERUT5.Text
                .PERUT6 = txtPERUT6.Text
                .PERIKSADALAM1 = txtPERIKSADALAM1.Text
                .PERIKSADALAM2 = txtPERIKSADALAM2.Text
                .PERIKSADALAM3 = txtPERIKSADALAM3.Text
                .PERIKSADALAM4 = txtPERIKSADALAM4.Text
                .PERIKSADALAM5 = txtPERIKSADALAM5.Text
                .LUKA = txtLUKA.Text

                 Try
                    Dim ms As New IO.MemoryStream()
                    picPERUT.Image.Save(ms, picPERUT.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .picPERUT = data
                 Catch oErr As Exception
                    Try
                        .picPERUT = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).picPERUT
                    Catch ex As Exception
                    End Try
                 End Try

                .KETERANGANPIC = txtKETERANGANPIC.Text
                .GINEKOLOGI1 = txtGINEKOLOGI1.Text
                .GINEKOLOGI2 = txtGINEKOLOGI2.Text
                .GINEKOLOGI3 = txtGINEKOLOGI3.Text
                .GINEKOLOGI4 = txtGINEKOLOGI4.Text
                .GINEKOLOGI5 = txtGINEKOLOGI5.Text
                .GINEKOLOGI6 = txtGINEKOLOGI6.Text
                .GINEKOLOGI7 = txtGINEKOLOGI7.Text
                .PALPASI1 = txtPALPASI1.Text
                .PALPASI2 = txtPALPASI2.Text
                .PALPASI3 = txtPALPASI3.Text
                .PALPASI4 = txtPALPASI4.Text
                .PALPASI5 = txtPALPASI5.Text
                .PALPASI6 = txtPALPASI6.Text
                .USG = txtUSG.Text
                .PEMERIKSAANPENUNJANG = txtPEMERIKSAANPENUNJANG.Text
                .DIFERENSIALDIAGNOSIS = txtDIFERENSIALDIAGNOSIS.Text
                .DIAGNOSISKERJA = txtDIAGNOSISKERJA.Text
                .PENGOBATANDANTINDAKAN = txtPENGOBATANDANTINDAKAN.Text
                .REKONSILIASIOBAT = txtREKONSILIASIOBAT.Text
                .DISCHARGEPLANNING = txtDISCHARGEPLANNING.Text

                Try
                    .CETAK = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_ASMEDOBGYN.GetData(txtNOREG.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With

             ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_ASMEDOBGYN.GetStructureDetailList ''
            For i As Integer = 0 To grvRiwayatObstetri.RowCount - 2
                Dim dsDetail = oS_DIGITAL_ASMEDOBGYN.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .TANGGALLAHIR = CDate(grvRiwayatObstetri.GetRowCellValue(i, colTL))
                    .JENISKELAMIN = grvRiwayatObstetri.GetRowCellValue(i, colJK)
                    .USIAKEHAMILAN = grvRiwayatObstetri.GetRowCellValue(i, colUsiaKehamilan)
                    .JENISPERSALINAN = grvRiwayatObstetri.GetRowCellValue(i, colJenisPersalinan)
                    .TEMPATDANPENOLONG = grvRiwayatObstetri.GetRowCellValue(i, colTempat)
                    .BBPB = grvRiwayatObstetri.GetRowCellValue(i, colBBPB)
                    .ASI = grvRiwayatObstetri.GetRowCellValue(i, colASI)
                    .KETERANGAN = grvRiwayatObstetri.GetRowCellValue(i, colKET)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASMEDOBGYN.InsertData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASMEDOBGYN.UpdateData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F6
                If btnDiagnosa.Enabled = True Then
                    btnDiagnosa_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub

    Private Sub btnReload_Click() Handles btnReload.ItemClick
                Dim dsKunjungan = oS_DIGITAL_ASMEDOBGYN.GetDataByKunjungan(txtNOREG.Text)

        Dim listPenunjang As New List(Of String)
        Dim listTindakanPengobatan As New List(Of String)

        If dsKunjungan IsNot Nothing Then
            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim oOrderLab As New Inventory.clsOrderLab
            Dim oOrderRad As New Inventory.clsOrderRad
            Dim oKonsul As New Digital.clsKonsul
            Dim oKonsulJawab As New Digital.clsJawabKonsul

            Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                             Join y In oOrderTindakan.GetDataDetail()
                             On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
                             Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderLab.GetDataDetail()
                        On x.KDORDERLAB Equals y.KDORDERLAB
                        Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderRad.GetDataDetail()
                        On x.KDORDERRAD Equals y.KDORDERRAD
                        Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                           Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                                Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsUnionPenunjang = dsLab.Union(dsRad)

            For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
                listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

            For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
                listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim oResep As New Inventory.clsOrderResep

            Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

            For Each xloop In dsResep
                For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
                    listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Next
            Next

            txtPEMERIKSAANPENUNJANG.Text = String.Join(", ", listPenunjang.ToArray)
            txtPENGOBATANDANTINDAKAN.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
    End Sub

    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
        If dsMasterDiagnosa IsNot Nothing Then
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        Else
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        End If

        If sCode = "Berhasil" Then
            Dim listDiagnosa As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
                listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
            Next

            Dim listProsedur As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
                listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
            Next
            txtDIAGNOSISKERJA.Text = String.Join(vbCrLf, listProsedur.ToArray)
            txtDIFERENSIALDIAGNOSIS.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        End If
    End Sub

    Private Sub chkPERNAHDIRAWATYA_CheckedChanged(sender As Object, e As EventArgs) Handles chkPERNAHDIRAWATYA.CheckedChanged
        If chkPERNAHDIRAWATYA.Checked Then
            chkPERNAHDIRAWATTIDAK.Checked = False
        End If
    End Sub

    Private Sub chkPERNAHDIRAWATTIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkPERNAHDIRAWATTIDAK.CheckedChanged
        If chkPERNAHDIRAWATTIDAK.Checked Then
            chkPERNAHDIRAWATYA.Checked = False
        End If
    End Sub

    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        'If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If MsgBox(txtNAMAISTRI.Text.Trim.ToUpper & " pasien Dokter :" & sDokter & vbCrLf & "Simpan Asesmen dengan User : " & sUserID & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        txtGINEKOLOGI1.Text = "dalam batas normal"
        txtGINEKOLOGI2.Text = "dalam batas normal"
        txtGINEKOLOGI3.Text = "dalam batas normal"
        txtGINEKOLOGI5.Text = "dalam batas normal"
        txtGINEKOLOGI6.Text = "dalam batas normal"
        txtGINEKOLOGI4.Text = "dalam batas normal"
        txtGINEKOLOGI7.Text = "dalam batas normal"
        txtPALPASI2.Text = "dalam batas normal"
        txtPALPASI6.Text = "dalam batas normal"
        'TextEdit11.Text = "dalam batas normal"
        'TextEdit12.Text = "dalam batas normal"
        'TextEdit13.Text = "dalam batas normal"
        'TextEdit14.Text = "dalam batas normal"
        'TextEdit15.Text = "dalam batas normal"
        'TextEdit16.Text = "dalam batas normal"
        'TextEdit17.Text = "dalam batas normal"
        'TextEdit18.Text = "dalam batas normal"
        'TextEdit19.Text = "dalam batas normal"
    End Sub

    Private _Previous As System.Nullable(Of Point) = Nothing
    Private Sub picPERUT_MouseDown(sender As Object, e As MouseEventArgs) Handles picPERUT.MouseDown
        _Previous = e.Location
        picPERUT_MouseMove(sender, e)
    End Sub

    Private Sub picPERUT_MouseMove(sender As Object, e As MouseEventArgs) Handles picPERUT.MouseMove
        If _Previous IsNot Nothing Then
            Dim GridColor As Color = Color.Red
            Dim GridPen As New Pen(GridColor)
            GridPen.Width = 2

            Using g As Graphics = Graphics.FromImage(picPERUT.Image)
                g.DrawLine(GridPen, _Previous.Value, e.Location)
            End Using
            picPERUT.Invalidate()
            _Previous = e.Location
        End If
    End Sub

    Private Sub pictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles picPERUT.MouseUp
        _Previous = Nothing
    End Sub


    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        picPERUT.Image = CType(My.Resources.ResourceManager.GetObject("picperut1"), Image)
    End Sub

    Private Sub frmEMedrekAsmedKebidanan_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub

#End Region
End Class
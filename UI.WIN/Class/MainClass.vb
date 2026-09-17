Imports System.IO
Imports System.Text
Imports DataAccess

Module MainClass
    Public Enum FORM_MODE
        FORM_MODE_VIEW = 0
        FORM_MODE_ADD = 1
        FORM_MODE_EDIT = 2
    End Enum

    Public menggunkanQR As Boolean = False
    Public listNoInvoiceAmbil As New List(Of String)
    'ByVal url As String, ByVal client_id As String, ByVal client_secret As String
    Public sInacbgTarifRawatJalanDefault As Decimal = 0
    Public sTanggalCPPT As DateTime = Now
    Public sTempatTTD As String = String.Empty
    Public SatuSehat_Production As Boolean = False
    Public SatuSehat_token As String = String.Empty
    'Public SatuSehat_Url As String = String.Empty
    Public SatuSehat_Organisasi As String = String.Empty
    Public SatuSehat_client_id As String = String.Empty
    Public SatuSehat_client_secret As String = String.Empty
    Public sUntukAlamatTTD As String = String.Empty
    Public sCetakEtiketFarmasi As Integer = 0
    Public sHasilLoadRadiologi As String = String.Empty
    Public USIA As String = String.Empty
    Public sKDCUSTOMER_RAD As String = String.Empty
    Public sNAME_DISPLAY_RAD As String = String.Empty
    Public sDATE_RAD As String = String.Empty
    Public sDEPARTMENT_RAD As String = String.Empty
    Public sREPORTDATE_RAD As String = String.Empty
    Public sEXAMPDESC_RAD As String = String.Empty
    Public sDOKTER_RAD As String = String.Empty
    Public sDIAGNOSA_RAD As String = String.Empty
    Public sDESCRIPTION_RAD As String = String.Empty
    Public sDOKTER_RAD2 As String = String.Empty
    Public sPEMERIKSAAN_RAD As String = String.Empty
    Public RM As String = String.Empty
    Public sUSIADIASESMENIGD As String = String.Empty
    Public sNAMA_DIKONSUL As String = String.Empty
    Public sRM_DIKONSUL As String = String.Empty
    Public sRUANG_DIKONSUL As String = String.Empty
    Public sDOKTERDARI_DIKONSUL As String = String.Empty
    Public sDOKTERKEPADA_DIKONSUL As String = String.Empty
    Public sISIKONSUL_DIKONSUL As String = String.Empty
    Public sJAWABKONSUL_DIKONSUL = String.Empty
    Public sWAKTU As String = String.Empty
    Public sASESEMEN_IGD As String = String.Empty
    Public NAMA As String = String.Empty
    Public JENISKELAMIN As String = String.Empty
    Public TANGGALLAHIR As String = String.Empty
    Public sKDUSER_TTD As String = String.Empty
    Public sAlamatTandaTanganDokter As String = String.Empty
    Public sConnOld As String = String.Empty
    Public AlamatDownloadIamge1 As String = String.Empty
    Public DownloadIamge1 As String = String.Empty
    Public sUserIDTandaTangan As String = String.Empty
    Public sKONSULTASI As String = String.Empty
    Public sUmurPasienDiCPPT As String = String.Empty
    Public sLoadAsesmenAwal As Boolean = False
    Public sLoadLaporanOperasi As Boolean = False
    Public sCPPT_QRRJ As Boolean = False

    Public sNoidSimpancppt As String = String.Empty
    Public sQueryMySQL As String = String.Empty
    Public sHargaApotik As Boolean = False
    Public sFind1_NomorRujukanAsalRujukan As String = String.Empty
    Public sFind1_NomorRujukan As String = String.Empty
    Public ListDiagnosaiDRG As New List(Of DataAccess.M_DIAGNOSA)
    Public ListProseduriDRG As New List(Of DataAccess.M_PROSEDUR)
    Public ListDiagnosaiNACBG As New List(Of DataAccess.M_DIAGNOSA)
    Public ListProseduriNACBG As New List(Of DataAccess.M_PROSEDUR)
    Public sFind1_cppt As String = String.Empty
    Public sFind2_cppt As String = String.Empty
    Public sFind3_cppt As String = String.Empty
    Public sNoTransaksi As String = String.Empty
    Public listCopy As New List(Of DataAccess.R_CPPT_NONRACIKAN)
    Public sSIMPAN As Boolean = False
    Public sRemarks_IntruksiDokter As String = String.Empty
    Public listdiagnosakodeidRG As New List(Of DataAccess.R_IDENTITAS_GROUPER_DATA_DIAGNOSAIDRG)
    Public listprosedurkodeidRG As New List(Of DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG)
    Public sGantiDiagnosa As Boolean = False
    Public sGantiProsedur As Boolean = False
    Public sJumlahProsedur As Integer = 0
    'Public ListDiagnosa As New List(Of DataAccess.M_DIAGNOSA)
    'Public ListProsedur As New List(Of DataAccess.M_PROSEDUR)
    Public sMySQL_Url As String = String.Empty
    Public sVclaim_Url As String = String.Empty
    Public sVclaim_ConsId As String = String.Empty
    Public sVclaim_SecreatKey As String = String.Empty
    Public sVclaim_UserKey As String = String.Empty
    Public sAntrol_Url As String = String.Empty
    Public sAntrol_ConsId As String = String.Empty
    Public sAntrol_SecreatKey As String = String.Empty
    Public sAntrol_UserKey As String = String.Empty
    Public sAplicare_Url As String = String.Empty
    Public sAplicare_ConsId As String = String.Empty
    Public sAplicare_SecreatKey As String = String.Empty
    Public sAplicare_UserKey As String = String.Empty
    Public sAplicare_PPK As String = String.Empty
    Public sEklaim_Url As String = String.Empty
    Public sEklaim_Generate As String = String.Empty

    Public sURLICARE As String = String.Empty

    Public sRMEBPJS_Url As String = String.Empty
    Public sRMEBPJS_ConsId As String = String.Empty
    Public sRMEBPJS_SecreatKey As String = String.Empty
    Public sRMEBPJS_UserKey As String = String.Empty
    Public sRMEBPJS_koderskemenkes As String = String.Empty

    Public sAlamatSimpanFolder As String = String.Empty

    Public sPPKPELAYANAN As String = String.Empty
    Public sModulBoolean As Boolean = False
    Public sAttacment_1
    Public sAttacment_2
    Public sAttachGambarPerut
    Public sTanggalPulang As DateTime = Now
    Public sJudulAsesmen As String = String.Empty
    Public sCaraPulang As String = String.Empty
    Public sKODEBOOKINGONLINEPANGGIL As String = String.Empty
    'Public sKARTUKODEBOKING As String = String.Empty
    Public sPoliDefault As String = String.Empty
    Public sCOPYKODDEBOOKING As String = String.Empty
    Public sALAMATTTD As String = String.Empty
    Public sALAMATTTDPASIEN As String = String.Empty
    Public sALAMATTTDGAMBAR As String = String.Empty
    Public sALAMATTTDGAMBAR_SIMPANFOLDER As String = String.Empty
    Public sALAMATTTDGAMBAR_SIMPANFOLDER_SEMENTARA As String = String.Empty
    Public sALAMATTTDGAMBAR_TTDPASIEN As String = String.Empty
    Public sALAMATTTDGAMBAR_TTDPASIEN_SEMENTARA As String = String.Empty
    Public sRemarksTemplate As String = String.Empty
    Public sKODETEMPLATE_RINGKASAN As String = String.Empty
    Public sKODETEMPLATE As String = String.Empty
    Public sKODECPPTCOPY As String = String.Empty
    Public sKODEASESMENCOPY As String = String.Empty
    Public sPicture As Image
    Public sPictureLogo As Image
    Public sPictureLogoSEP As Image
    Public sNAMARS As String = String.Empty
    Public sUSIA As String = String.Empty
    Public sSIPNIP As String = String.Empty
    Public sPoli As String = String.Empty
    'Public sAlasanSKD As String = String.Empty
    'Public sTindakLanjutSKD As String = String.Empty
    Public sUserKTP As String = String.Empty
    Public sBelumPanggil As Integer = 1
    'Public sConnNpgsqlproduction As String = "Host=36.92.89.156;Port=5433;Username=postgres;Password=db9untur;Database=rstni_guntur_prod"
    'Public sConnNpgsqlproduction As String = String.Empty
    'Public sConnMySql As String = "Data Source=p1.mail-sender.my.id;Initial Catalog=daftar;Persist Security Info=True;User ID=remote;Password=ocdny68gascsaxkxnsavxa; Port = 3306;"
    Public sConnMySqlAntrianOnline As String = ""
    Public sANTRIAN As Boolean = False
    Public sPOSTRAWAT As Boolean = False
    Public sUMURPASIEN As String = String.Empty
    Public sASALRUJUKAN_ANTRIAN As String = String.Empty
    Public sNOMORREFERENSI_ANTRIAN As String = String.Empty
    Public sKODEANTRIAN_ANTRIAN As String = String.Empty
    Public sKDCUSTOMER_ANTRIAN As String = String.Empty
    Public sJENISKUNJUNGAN_ANTRIAN As String = String.Empty
    Public sNomorSEPKartu As String = String.Empty
    Public sNomorSKDPspri As String = String.Empty
    Public sNomorSKDPspri_SEP As String = String.Empty
    Public sNomorSKDPspriSEP As String = String.Empty
    Public sNomorSKDPspri_TanggalSKD As String = String.Empty
    Public sNomorSKDPspri_NamaDokter As String = String.Empty
    Public sNomorSKDPspri_Poli As String = String.Empty
    'Public sAktiveVersi2 As Boolean = False
    'Public USER_KEY As String = String.Empty
    'Public USER_KEY_ANTRIAN As String = String.Empty
    Public sCATEGORYSETOR_ As String = String.Empty
    Public sCATEGORYSETOR As String = String.Empty
    Public sBayar As Boolean = False
    Public sPesanHapus As String = String.Empty
    Public sCopySEPdiSKD As String = String.Empty
    Public sSHIFT As String = String.Empty
    Public sKDCUSTOMERX As String = String.Empty
    Public sKD_PASIENnci As String = String.Empty
    Public sKD_PASIENdgCare As String = String.Empty
    Public sUmur As String = String.Empty
    Public sStok As Boolean = False
    Public sStokUom As Boolean = False
    Public sCategoryAbsensi As Integer = 0
    Public sCetakSEP As Boolean = False
    Public sUserID As String = String.Empty
    Public sTerbilang As Decimal = 0
    Public sCode As String = String.Empty
    Public sCodeSKD As String = String.Empty
    Public sCodeCustomer As String = String.Empty
    Public sStatusSave As String = String.Empty
    Public sDaftar_L1 As String = String.Empty
    Public sDaftar_L2 As String = String.Empty
    Public sDaftar_L3 As String = String.Empty
    Public sDaftar_L4 As String = String.Empty
    Public sDaftar_L5 As String = String.Empty
    Public sDaftar_L6 As String = String.Empty
    Public sItem_L1 As String = String.Empty
    Public sItem_L2 As String = String.Empty
    Public sItem_L3 As String = String.Empty
    Public sItem_L4 As String = String.Empty
    Public sItem_L5 As String = String.Empty
    Public sItem_L6 As String = String.Empty
    Public sPrintSubtotal As Decimal = 0
    Public sPrintPotongan As Decimal = 0
    Public sPrintGrandTotal As Decimal = 0
    Public sCostShare As Boolean = False
    Public sPrintCashierDescriprtion As String = String.Empty
    Public sREKAMMEDIS As String = String.Empty
    Public sVersion As Integer = 0
    Public sFind1 As String = String.Empty
    Public sFind2 As String = String.Empty
    Public sFind3 As String = String.Empty
    Public sFind4 As String = String.Empty
    Public sFind5 As String = String.Empty
    Public sFind6 As String = String.Empty
    Public sFind7 As String = String.Empty
    Public sFind8 As String = String.Empty
    Public sFind9 As String = String.Empty
    Public sFind10 As String = String.Empty
    Public sCompany As String = String.Empty
    Public sAddress As String = String.Empty
    Public sPhone As String = String.Empty
    Public sNPWP As String = String.Empty
    Public sJenisDaftar As String = String.Empty
    Public sKDSERVER As String = String.Empty
    Public sKDDATABASE As String = String.Empty
    Public sKDSERVER_NEW As String = String.Empty
    Public sKDDATABASE_NEW As String = String.Empty
    Public sKDSERVER_TAX As String = String.Empty
    Public sKDDATABASE_TAX As String = String.Empty
    Public sKDCOMPANY As String = String.Empty
    Public sWATERMARK As String = ""
    Public sSavePayment As Boolean = False
    Public sDATE1 As DateTime = Now
    Public sDATE2 As DateTime = Now
    Public sCustomer As String = String.Empty
    Public sdelivery_method As String = String.Empty
    Public sdelivery_dttm As DateTime = Now
    Public sletak_janin As String = String.Empty
    Public skondisi As String = String.Empty
    Public suse_manual As String = String.Empty
    Public suse_forcep As String = String.Empty
    Public suse_vacuum As String = String.Empty
    Public sshk_spesimen_ambil As String = String.Empty
    Public sshk_lokasi As String = String.Empty
    Public sshk_spesimen_dttm As DateTime = Now
    Public sshk_alasan As String = String.Empty
    Public sRemarks_Ruangan As String = String.Empty
    Public sRemarks_RencanaPembedahan As String = String.Empty
    Public sRemarks As String = String.Empty

    Public sJudulLab1 As String = String.Empty
    Public sJudulLab2 As String = String.Empty
    Public sJudulLab3 As String = String.Empty
    Public sJudulLab4 As String = String.Empty
    'Public sRemarks_IntruksiDokter As String = String.Empty
#Region "Printing"
    Public Sub pLine_(ByRef oTxtStream As TextWriter, ByVal nSPACE As Integer, ByVal xcCHAR As String)
        Dim cLINE As String

        cLINE = Space(nSPACE)
        cLINE = Replace(cLINE, " ", xcCHAR)
        oTxtStream.WriteLine(cLINE)
    End Sub
    Public Function pSpace_(ByRef cText As String, ByVal nLength As Long, ByVal Alignment As Single) As String
        Dim cResult As String, nTextLength As Long

        nTextLength = Len(cText)
        cResult = cText
        If (nTextLength < nLength) Then
            cResult = cResult & Space(nLength - Len(cResult))
        ElseIf (nTextLength > nLength) Then
            cResult = Mid(cResult, 1, nLength)
            If Len(cResult) < nLength Then
                cResult = cResult & Space(nLength - Len(cResult))
            End If
        End If
        If (nTextLength < nLength) Then
            Select Case Alignment
                Case 1
                    cResult = (Space(nLength - nTextLength) + cText)
                Case 2
                    cResult = (Space((nLength - nTextLength) / 2) + cText)
                Case 3
                    cResult = (cText + Space(nLength - nTextLength))
            End Select
            cResult = cResult + Space(nLength - Len(cResult))
        End If
        pSpace_ = cResult
        pSpace_ = cResult
    End Function
#End Region

#Region "Encrypt / Decrypt"
    Private Const INT_lens As Integer = 1
    Public str As StringBuilder
    Public searchStr As String
    Public b As Integer = 6
    Public p() As Integer = {2, 4, 7, 9, 3, INT_lens}
    Public i As Integer
    Public j As Integer
    Public k As Integer
    Public c As Integer
    Public lens As Integer

    Public Function Encrypt(ByVal inputstr As String) As String
        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While
        For i As Integer = 0 To ((lens / b) - INT_lens)
            For j As Integer = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) + k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function
    Public Function Decrypt(ByVal inputstr As String) As String

        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While

        For i As Integer = 0 To ((lens / b) - INT_lens)
            For j As Integer = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) - k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function
#End Region
End Module

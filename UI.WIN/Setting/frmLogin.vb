Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports DevExpress.LookAndFeel

Public Class frmLogin
    Private ID As String = String.Empty
    Private sPassword As String = String.Empty

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oConnection As New Setting.clsConnectionUser

        If Not oConnection.GetConnection() Then
            frmDatabaseUser.ShowDialog()
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "") <> Nothing Then
                Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "").ToString()).Split(";")

                For iLoop As Integer = 0 To arrMain.Length - 1
                    Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                    For xLoop As Integer = 0 To arrMainResult.Length - 1
                        If arrMainResult(xLoop) = "Data Source" Then
                            'txtMainServer.Text = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "Initial Catalog" Then
                            'txtMainDatabase.Text = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "User ID" Then
                            ID = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "Password" Then
                            sPassword = arrMainResult(xLoop + 1)
                        End If
                    Next
                Next
            End If
        End If
    End Sub
#Region "Function"
    Public Function fn_Login() As Boolean
        fn_Login = False

        If sANTRIAN = True Then
            txtUsername.Text = "ANTRIAN"
            txtPassword.Text = "ANTRIAN"
        End If
        Try
            Dim oUser As New Setting.clsUser
            If oUser.GetData(txtUsername.Text.Trim.ToUpper) IsNot Nothing Then
                Dim ds = oUser.GetData(txtUsername.Text.Trim.ToUpper)
                With ds
                    If txtUsername.Text.Trim.ToUpper = .KDUSER And txtPassword.Text = .PASSWORD Then
                        UserLookAndFeel.Default.SkinName = .SKINDEFAULT

                        sUserID = .KDUSER.ToUpper
                        sSHIFT = "PAGI"
                        sUserKTP = ds.NIK

                        Dim oConnectionMain As New Setting.clsConnectionMain

                        Try
                            oConnectionMain.SaveToRegistry(Encrypt("Data Source=" & ds.KDSERVER & ";Initial Catalog=" & ds.KDDATABASE & ";Persist Security Info=True;User ID=" & ID & ";Password=" & sPassword & ""))
                        Catch ex As Exception

                        End Try
                        Try
                            oConnectionMain.SaveToRegistryTax(Encrypt("Data Source=" & ds.KDSERVER_TAX & ";Initial Catalog=" & ds.KDDATABASE_TAX & ";Persist Security Info=True;User ID=" & ID & ";Password=" & sPassword & ""))
                        Catch ex As Exception

                        End Try
                        Try
                            oConnectionMain.SaveToRegistryRME(Encrypt("Data Source=" & ds.KDSERVER_NEW & ";Initial Catalog=" & ds.KDDATABASE_NEW & ";Persist Security Info=True;User ID=" & ID & ";Password=" & sPassword & ""))
                        Catch ex As Exception

                        End Try

                        sCompany = ds.SET_COMPANY.COMPANY
                        sAddress = ds.SET_COMPANY.ADDRESS
                        sPhone = ds.SET_COMPANY.PHONE
                        sNPWP = ds.SET_COMPANY.NPWP

                        sKDSERVER = ds.KDSERVER
                        sKDDATABASE = ds.KDDATABASE
                        sKDSERVER_TAX = ds.KDSERVER_TAX
                        sKDDATABASE_TAX = ds.KDDATABASE_TAX
                        sKDCOMPANY = ds.KDCOMPANY
                        sKDSERVER_NEW = ds.KDSERVER_NEW
                        sKDDATABASE_NEW = ds.KDDATABASE_NEW

                        fn_Login = True

                        'Dim oSetKoneksi As New Brigging.clsSetKoneksi
                        'Dim dsKoneksi = oSetKoneksi.GetDataAktiveByNameDisplay("VCLAIM2")
                        'If dsKoneksi IsNot Nothing Then
                        '    sAktiveVersi2 = dsKoneksi.ISACTIVEVERSI2
                        '    USER_KEY = dsKoneksi.USER_KEY
                        '    sPPKPELAYANAN = dsKoneksi.PPKPELAYANAN
                        'Else
                        '    sAktiveVersi2 = False
                        'End If

                        'Dim dsKoneksiAntrol = oSetKoneksi.GetDataAktiveByNameDisplay("ANTREAN")
                        'If dsKoneksiAntrol IsNot Nothing Then
                        '    USER_KEY_ANTRIAN = dsKoneksiAntrol.USER_KEY
                        'End If

                        'Dim dsOld = oSetKoneksi.GetDataAktiveByNameDisplay("KONEKSIOLD")
                        'If dsOld IsNot Nothing Then
                        '    sConnNpgsqlproduction = dsOld.REMARKS
                        '    sConnMySqlAntrianOnline = dsOld.ALAMATWEB
                        'End If

                        Dim dsVclaim = oUser.GetDataKoneksiBPJS("VCLAIM")
                        If dsVclaim IsNot Nothing Then
                            sVclaim_Url = dsVclaim.ALAMATWEB
                            sVclaim_ConsId = dsVclaim.CONSID
                            sVclaim_SecreatKey = dsVclaim.SECREATKEY
                            sVclaim_UserKey = dsVclaim.REMARKS
                            sPPKPELAYANAN = dsVclaim.PPKPELAYANAN
                        End If

                        Dim dsAntrol = oUser.GetDataKoneksiBPJS("ANTREAN")
                        If dsAntrol IsNot Nothing Then
                            sAntrol_Url = dsAntrol.ALAMATWEB
                            sAntrol_ConsId = dsAntrol.CONSID
                            sAntrol_SecreatKey = dsAntrol.SECREATKEY
                            sAntrol_UserKey = dsAntrol.REMARKS
                        End If

                        Dim dsAPlicare = oUser.GetDataKoneksiBPJS("APLICARE")
                        If dsAPlicare IsNot Nothing Then
                            sAplicare_Url = dsAPlicare.ALAMATWEB
                            sAplicare_ConsId = dsAPlicare.CONSID
                            sAplicare_SecreatKey = dsAPlicare.SECREATKEY
                            sAplicare_UserKey = dsAPlicare.REMARKS
                            sAplicare_PPK = dsAPlicare.PPKPELAYANAN
                        End If

                        Dim dsEklaim = oUser.GetDataKoneksiBPJS("EKLAIM")
                        If dsEklaim IsNot Nothing Then
                            sEklaim_Url = dsEklaim.ALAMATWEB
                            sEklaim_Generate = dsEklaim.REMARKS
                        End If

                        Dim dsMySQL = oUser.GetDataKoneksiBPJS("MYSQL")
                        If dsMySQL IsNot Nothing Then
                            If dsMySQL.ISACTIVE = True Then
                                sMySQL_Url = dsMySQL.ALAMATWEB
                                sQueryMySQL = dsMySQL.REMARKS
                            Else
                                sMySQL_Url = ""
                                sQueryMySQL = ""
                            End If
                        End If

                        Dim dsAlamatTTD = oUser.GetDataImageKode("FOLDERTTD")
                        If dsAlamatTTD IsNot Nothing Then
                            sALAMATTTD = dsAlamatTTD.ALAMAT
                        End If

                        Dim dsQR = oUser.GetDataImageKode("MENGGUNAKANQR")
                        If dsQR IsNot Nothing Then
                            If dsQR.ALAMAT = "YA" Then
                                menggunkanQR = True
                            End If
                        End If

                        Dim dsAlamatTTDPASIEN = oUser.GetDataImageKode("FOLDERTTDPASIEN")
                        If dsAlamatTTDPASIEN IsNot Nothing Then
                            sALAMATTTDPASIEN = dsAlamatTTDPASIEN.ALAMAT
                        End If

                        Dim dsAlamatGambar = oUser.GetDataImageKode("FOLDERGAMBAR")
                        If dsAlamatGambar IsNot Nothing Then
                            sALAMATTTDGAMBAR = dsAlamatGambar.ALAMAT
                        End If

                        Dim dsAlamatdiTTD = oUser.GetDataImageKode("ALAMATDITTD")
                        If dsAlamatdiTTD IsNot Nothing Then
                            sUntukAlamatTTD = dsAlamatdiTTD.ALAMAT
                        End If

                        Dim dsLogo = oUser.GetDataImageKode("LOGO")
                        If dsLogo IsNot Nothing Then
                            Try
                                Dim img = dsLogo.GAMBAR
                                sPictureLogo = ByteArrayToImage(img.ToArray())
                            Catch oErr As Exception
                                sPictureLogo = Nothing
                            End Try
                        End If

                        Dim dsLogoSEP = oUser.GetDataImageKode("SEP")
                        If dsLogoSEP IsNot Nothing Then
                            Try
                                sNAMARS = dsLogoSEP.ALAMAT
                                Dim img = dsLogoSEP.GAMBAR
                                sPictureLogoSEP = ByteArrayToImage(img.ToArray())
                            Catch oErr As Exception
                                sPictureLogoSEP = Nothing
                            End Try
                        End If

                        Dim dsSimpanGambar = oUser.GetDataImageKode("SIMPANGAMBAR")
                        If dsSimpanGambar IsNot Nothing Then
                            sAlamatSimpanFolder = dsSimpanGambar.ALAMAT
                        End If

                        Dim dsHarga = oUser.GetDataKoneksiBPJS("HARGA")
                        If dsHarga IsNot Nothing Then
                            If dsHarga.ISACTIVE = True Then
                                sHargaApotik = True
                            Else
                                sHargaApotik = False
                            End If

                            Try
                                sInacbgTarifRawatJalanDefault = dsHarga.CONSID
                            Catch ex As Exception

                            End Try
                        Else
                            sHargaApotik = False
                        End If

                        Dim dsEtiket = oUser.GetDataKoneksiBPJS("ETIKETFARMASI")
                        If dsEtiket IsNot Nothing Then
                            Try
                                sCetakEtiketFarmasi = dsEtiket.NAME_DISPLAY
                            Catch ex As Exception
                                sCetakEtiketFarmasi = 0
                            End Try
                        End If

                        Dim dsTTDTEMPAT = oUser.GetDataKoneksiBPJS("LOKASITTD")
                        If dsTTDTEMPAT IsNot Nothing Then
                            sTempatTTD = dsTTDTEMPAT.NAME_DISPLAY
                        Else
                            sTempatTTD = "Bandung Barat"
                        End If
                        Dim dsRMEBPJS = oUser.GetDataKoneksiBPJS("RMEBPJS")
                        If dsRMEBPJS IsNot Nothing Then
                            sRMEBPJS_Url = dsRMEBPJS.ALAMATWEB
                            sRMEBPJS_ConsId = dsRMEBPJS.CONSID
                            sRMEBPJS_SecreatKey = dsRMEBPJS.SECREATKEY
                            sRMEBPJS_UserKey = dsRMEBPJS.REMARKS
                            sRMEBPJS_koderskemenkes = dsRMEBPJS.PPKPELAYANAN
                        End If

                        Dim oSatuSehatKoneksi As New Setting.clsSatuSehatKoneksi
                        Dim dsSatuSehatKoneksi = oSatuSehatKoneksi.GetDataAmbilKoneksi()
                        If dsSatuSehatKoneksi IsNot Nothing Then
                            'SatuSehat_Url = dsSatuSehatKoneksi.MEMO
                            SatuSehat_Organisasi = dsSatuSehatKoneksi.ORGANIZATIONID
                            SatuSehat_client_id = dsSatuSehatKoneksi.CLIENTID
                            SatuSehat_client_secret = dsSatuSehatKoneksi.CLIENTSECRET

                            Dim oToken As New Setting.clsSatuSehatKoneksiToken

                            Dim dsToken = oToken.GetDataSEQ(dsSatuSehatKoneksi.KDKONEKSI)
                            If dsToken IsNot Nothing Then
                                SatuSehat_token = dsToken.GENERATETOKEN
                            End If

                            If dsSatuSehatKoneksi.KDKONEKSI = "SANDBOX" Then
                                SatuSehat_Production = False
                            ElseIf dsSatuSehatKoneksi.KDKONEKSI = "PRODUCTION"
                                SatuSehat_Production = True
                            Else
                                SatuSehat_Organisasi = ""
                            End If
                        End If

                        Dim dsiCare = oUser.GetDataKoneksiBPJS("ICARE")
                        If dsiCare IsNot Nothing Then
                            sURLICARE = dsiCare.ALAMATWEB
                        End If

                        Dim dsJudulLaboratorium = oUser.GetDataKoneksiBPJS("JUDULLAB")
                        If dsJudulLaboratorium IsNot Nothing Then
                            sJudulLab1 = dsJudulLaboratorium.NAME_DISPLAY
                            sJudulLab2 = dsJudulLaboratorium.PPKPELAYANAN
                            sJudulLab3 = dsJudulLaboratorium.CONSID
                            sJudulLab4 = dsJudulLaboratorium.ALAMATWEB
                        End If

                        Exit Function
                    Else
                        MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                        txtUsername.Focus()
                        Exit Function
                    End If
                End With
            Else
                MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                txtUsername.Focus()
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
#End Region

#Region "Command Button"
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        sANTRIAN = False
        If fn_Login() Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Environment.Exit(1)
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sANTRIAN = True
        If fn_Login() Then
            Me.Close()
        End If
    End Sub

#End Region
End Class
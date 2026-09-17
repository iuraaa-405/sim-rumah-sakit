Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_RM_02
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New EMedrek.clsS_DIGITAL_RM_02
    Private sKoneksi As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        deDateLahir.DateTime = Now
        oFormMode = FormMode

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)
        If dsPendaftaran IsNot Nothing Then
            txtKDREG.Text = dsPendaftaran.KDKUNJUNGAN
            txtRM.Text = dsPendaftaran.KDCUSTOMER
            txtNAMAPASIEN.Text = dsPendaftaran.NAMAPASIEN.Trim.ToUpper
            deDATELAHIRPASIEN.DateTime = dsPendaftaran.TANGGALLAHIR

            If dsPendaftaran.JENISKELAMIN = "L" Then
                cboJKPasien.SelectedIndex = 0
            Else
                cboJKPasien.SelectedIndex = 1
            End If
        Else
            txtKDREG.ResetText()
            txtRM.ResetText()
            txtNAMAPASIEN.ResetText()
            deDATELAHIRPASIEN.DateTime = Now
            cboJKPasien.SelectedIndex = 0
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDREG.Text
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadNoHubungan()

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

    End Sub
    Private Sub fn_EmptyMe()
        txtNama.ResetText()
        deDateLahir.ResetText()
        cboJK.ResetText()
        txtAlamat.ResetText()
        grdNoHubungan.ResetText()
        rbStatus.ResetText()
        cboJK.SelectedIndex = 0

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim dsDetail_1 = oDigital.GetData(txtKDREG.Text)
            With dsDetail_1
                txtKDREG.Text = txtKDREG.Text
                txtNama.Text = .NAMA
                deDateLahir.DateTime = .TANGGALLAHIR
                cboJK.SelectedIndex = .JENISKELAMIN
                txtAlamat.Text = .ALAMAT
                grdNoHubungan.Text = .HUBUNGAN_KODE
                rbStatus.SelectedIndex = .KODE
            End With

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDREG.Text = String.Empty Then
                MsgBox("Dibutuhkan KDREG", MsgBoxStyle.Exclamation, Me.Text)
                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdNoHubungan.Text = String.Empty Then
                MsgBox("Dibutuhkan Hubungan", MsgBoxStyle.Exclamation, Me.Text)
                grdNoHubungan.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtAlamat.Text = String.Empty Then
                MsgBox("Dibutuhkan Alamat", MsgBoxStyle.Exclamation, Me.Text)
                txtAlamat.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

            Dim ds_2 = oDigital.GetStructureHeader

            With ds_2
                .KDKUNJUNGAN = txtKDREG.Text
                Try
                    .DATECREATED = oDigital.GetData(txtKDREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .NAMA = txtNama.Text.Trim.ToUpper
                .TANGGALLAHIR = deDateLahir.DateTime
                .JENISKELAMIN = cboJK.SelectedIndex
                .ALAMAT = txtAlamat.Text.Trim.ToUpper
                .HUBUNGAN_KODE = grdNoHubungan.EditValue.ToString.Trim.ToUpper
                .HUBUNGAN_NAMEDISPLAY = grdNoHubungan.Text.ToString.Trim.ToUpper
                .KODE = rbStatus.SelectedIndex
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE

                Try
                    .CETAK = oDigital.GetData(txtKDREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds_2)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(ds_2)
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadNoHubungan()
        Try
            Dim sKoneksi As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksi = dsSetKoneksi.GENERATE_ECLAIM
            End If

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
            SQL &= "M_NOHUBUNGAN A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HUBUNGAN")

            grdNoHubungan.Properties.DataSource = ds.Tables("HUBUNGAN")
            grdNoHubungan.Properties.ValueMember = "KDNOHUBUNGAN"
            grdNoHubungan.Properties.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSuKet_HD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private sNoId As String
    Private oSuKet_HD As New Digital.clsDigital_SUKET_HD
    Private sKoneksi As String = String.Empty
    Private sKDDOCTOR As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        If dsPendaftaran IsNot Nothing Then
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtNOBPJS.Text = dsPendaftaran.KARTUBPJS
            txtAlamat.Text = dsPendaftaran.ALAMAT
            txtKunjungan.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRegister.Text = dsPendaftaran.KDPENDAFTARAN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            sKDDOCTOR = dsPendaftaran.KDDOKTER
        Else
            txtNoRM.ResetText()
            txtNamaPasien.ResetText()
            txtNOBPJS.ResetText()
            txtAlamat.ResetText()
            txtKunjungan.ResetText()
            txtNoRegister.ResetText()
            txtJK.ResetText()
            sKDDOCTOR = ""
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "SURAT KETERANGAN HEMODIALISA"

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetDataVCLaim()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOMOR.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
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

        deDATE.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        grdDOKTER.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtNOMOR.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdDOKTER.EditValue = sKDDOCTOR
        fn_LoadDiagnosa(txtNoRegister.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuKet_HD.GetData(sNoId)

            With ds
                txtNOMOR.Text = .KDSKHD
                deDATE.DateTime = .DATE
                txtDIAGNOSA.Text = .DIAGNOSA
                grdDOKTER.Text = .DOCTOR_KODE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
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
            Dim ds = oSuKet_HD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSuKet_HD.GetData(sNoId).DATECREATED                   
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDSKHD = sNoId
                .KDKUNJUNGAN = txtKunjungan.Text
                .NAMA = txtNamaPasien.Text
                .NOBPJS = txtNOBPJS.Text
                .DIAGNOSA = txtDIAGNOSA.Text
                .DOCTOR_KODE = grdDOKTER.EditValue
                .DOCTOR_NAMEDISPLAY = grdDOKTER.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSuKet_HD.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuKet_HD.UpdateData(ds)
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
        End Select
    End Sub

    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDiagnosa(ByVal Parameter As String)
        Try
            Dim oDiagnosaMaster As New Diagnosa.clsMasterDiagnosa
            Dim dsDiagnosa = oDiagnosaMaster.GetDataByKDpendaftaran(Parameter)

            If dsDiagnosa IsNot Nothing Then
                txtDIAGNOSA.Text = dsDiagnosa.REMARKS
            End If

        Catch ex As Exception

        End Try
    End Sub
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOKTER.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTER.Properties.ValueMember = "KDDOCTOR"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCCPTDokterList
    Implements ILanguage
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private sNoRM As String = String.Empty

#Region "Function"
    Public Sub fn_LoadNoRM(ByVal Parameter As String)
        sNoRM = Parameter
    End Sub
    Private Sub Me_Load() Handles Me.Load
        sNoidSimpancppt = String.Empty
        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
                     And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picUpdate.Enabled = ds.ISUPDATE
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picUpdate.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "CPPT Dokter List"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oGrouperDataCppt.GetDataListMedis(sNoRM)
        '             Select TANGGAL = x.DATE, x.KDCPPT, x.KDUSER

        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KATEGORI = 'CPPT' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",RUANGAN = B.KDDEPARTMENT_NAMA "
            SQL &= ",A.KDCPPT "
            SQL &= ",A.KDUSER "
            SQL &= ",HANDOVERPEMBERI = ISNULL((SELECT KDUSER FROM R_CPPT_HANDOVER_PEMBERI WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= ",HANDOVERPENERIMA = ISNULL((SELECT KDUSER FROM R_CPPT_HANDOVER_PENERIMA WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= ",NILAIKRITISPEMBERI = ISNULL((SELECT KDUSER FROM R_CPPT_NILAIKRITIS_PEMBERI WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= ",NILAIKRITISPENERIMA = ISNULL((SELECT KDUSER FROM R_CPPT_NILAIKRITIS_PENERIMA WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= ",SBARPEMBERI = ISNULL((SELECT KDUSER FROM R_CPPT_SBAR_PEMBERI WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= ",SBARPENERIMA = ISNULL((SELECT KDUSER FROM R_CPPT_SBAR_PENERIMA WHERE A.KDCPPT = KDCPPT), '') "
            SQL &= "FROM "
            SQL &= "R_CPPT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDCUSTOMER = '" & sNoRM & "' "
            SQL &= "AND B.CATEGORY = 1 "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_CPPT")

            grd.DataSource = ds.Tables("R_CPPT")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("KDCPPT").Visible = False
        grv.Columns("KDCPPT").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        grv.ShowCustomization()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            'Case Keys.A
            '    If e.Alt = True And picAdd.Enabled = True Then
            '        picAdd_Click()
            '    End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            'Case Keys.D
            '    If e.Alt = True And picDelete.Enabled = True Then
            '        picDelete_Click()
            '    End If
            'Case Keys.P
            '    If e.Alt = True And picPrint.Enabled = True Then
            '        picPrint_Click()
            '    End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("User Tidak Sesuai, tidak bisa edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        sNoidSimpancppt = grv.GetFocusedRowCellValue("KDCPPT")
        Me.Close()
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub PemberiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_HandOver_Pemberi As New EMedrek.clsCppt_HandOver_Pemberi

        Dim ds = oCppt_HandOver_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTHandOverPemberi As New frmCPPTHandOverPemberi
            Try
                frmCPPTHandOverPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTHandOverPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPemberi Is Nothing Then frmCPPTHandOverPemberi.Dispose()
                frmCPPTHandOverPemberi = Nothing

            End Try
        Else
            Dim frmCPPTHandOverPemberi As New frmCPPTHandOverPemberi
            Try
                frmCPPTHandOverPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTHandOverPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPemberi Is Nothing Then frmCPPTHandOverPemberi.Dispose()
                frmCPPTHandOverPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("HANDOVERPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Hand Over", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_HandOver_Penerima As New EMedrek.clsCppt_HandOver_Penerima

        Dim ds = oCppt_HandOver_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTHandOverPenerima As New frmCPPTHandOverPenerima
            Try
                frmCPPTHandOverPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTHandOverPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPenerima Is Nothing Then frmCPPTHandOverPenerima.Dispose()
                frmCPPTHandOverPenerima = Nothing

            End Try
        Else
            Dim frmCPPTHandOverPenerima As New frmCPPTHandOverPenerima
            Try
                frmCPPTHandOverPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTHandOverPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPenerima Is Nothing Then frmCPPTHandOverPenerima.Dispose()
                frmCPPTHandOverPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub PemberiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem1.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_NilaiKritis_Pemberi As New EMedrek.clsCppt_NilaiKritis_Pemberi

        Dim ds = oCppt_NilaiKritis_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTNilaiKritisPemberi As New frmCPPTNilaiKritisPemberi
            Try
                frmCPPTNilaiKritisPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTNilaiKritisPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPemberi Is Nothing Then frmCPPTNilaiKritisPemberi.Dispose()
                frmCPPTNilaiKritisPemberi = Nothing

            End Try
        Else
            Dim frmCPPTNilaiKritisPemberi As New frmCPPTNilaiKritisPemberi
            Try
                frmCPPTNilaiKritisPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTNilaiKritisPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPemberi Is Nothing Then frmCPPTNilaiKritisPemberi.Dispose()
                frmCPPTNilaiKritisPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem1.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("NILAIKRITISPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Nilai Kritis", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_NilaiKritis_Penerima As New EMedrek.clsCppt_NilaiKritis_Penerima

        Dim ds = oCppt_NilaiKritis_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTNilaiKritisPenerima As New frmCPPTNilaiKritisPenerima
            Try
                frmCPPTNilaiKritisPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTNilaiKritisPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPenerima Is Nothing Then frmCPPTNilaiKritisPenerima.Dispose()
                frmCPPTNilaiKritisPenerima = Nothing

            End Try
        Else
            Dim frmCPPTNilaiKritisPenerima As New frmCPPTNilaiKritisPenerima
            Try
                frmCPPTNilaiKritisPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTNilaiKritisPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPenerima Is Nothing Then frmCPPTNilaiKritisPenerima.Dispose()
                frmCPPTNilaiKritisPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub PemberiToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem2.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_SBAR_Pemberi As New EMedrek.clsCppt_SBAR_Pemberi

        Dim ds = oCppt_SBAR_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTSBARPemberi As New frmCPPTSBARPemberi
            Try
                frmCPPTSBARPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTSBARPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPemberi Is Nothing Then frmCPPTSBARPemberi.Dispose()
                frmCPPTSBARPemberi = Nothing

            End Try
        Else
            Dim frmCPPTSBARPemberi As New frmCPPTSBARPemberi
            Try
                frmCPPTSBARPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTSBARPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPemberi Is Nothing Then frmCPPTSBARPemberi.Dispose()
                frmCPPTSBARPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem2.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("SBARPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Nilai Kritis", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_SBAR_Penerima As New EMedrek.clsCppt_SBAR_Penerima

        Dim ds = oCppt_SBAR_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTSBARPenerima As New frmCPPTSBARPenerima
            Try
                frmCPPTSBARPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTSBARPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPenerima Is Nothing Then frmCPPTSBARPenerima.Dispose()
                frmCPPTSBARPenerima = Nothing

            End Try
        Else
            Dim frmCPPTSBARPenerima As New frmCPPTSBARPenerima
            Try
                frmCPPTSBARPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTSBARPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPenerima Is Nothing Then frmCPPTSBARPenerima.Dispose()
                frmCPPTSBARPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub VerifikasiDPJPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerifikasiDPJPToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_Verifikasi As New EMedrek.clsCppt_Verifikasi

        Dim ds = oCppt_Verifikasi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTVerifikasi As New frmCPPTVerifikasi
            Try
                frmCPPTVerifikasi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTVerifikasi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTVerifikasi Is Nothing Then frmCPPTVerifikasi.Dispose()
                frmCPPTVerifikasi = Nothing

            End Try
        Else
            Dim frmCPPTVerifikasi As New frmCPPTVerifikasi
            Try
                frmCPPTVerifikasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTVerifikasi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTVerifikasi Is Nothing Then frmCPPTVerifikasi.Dispose()
                frmCPPTVerifikasi = Nothing

            End Try
        End If
    End Sub
#End Region
End Class
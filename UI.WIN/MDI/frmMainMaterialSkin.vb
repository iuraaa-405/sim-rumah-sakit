Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports DevExpress.XtraWaitForm

Public Class frmMainMaterialSkin
    Private sMaster As String = "Obat"

    Private Sub frmMainMaterialSkin_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        fn_LoadLogin()
        MaterialTabControl1_SelectedIndexChanged()
    End Sub
    Private Sub frmMainMaterialSkin_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (MessageBox.Show(Statement.ExitApplication, Caption.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.Yes Then
            For Each iLoop In Me.MdiChildren
                iLoop.Close()
            Next

            Environment.Exit(1)
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub fn_LoadLogin()
        frmLogin.ShowDialog()
    End Sub
    Private Sub MaterialTabControl1_SelectedIndexChanged() Handles MaterialTabControl1.SelectedIndexChanged
        If MaterialTabControl1.SelectedIndex = 0 Then
            Me.Text = "Home"

            TabPageHome.Dock = DockStyle.None

            TabPageHome.Controls.Clear()

            ' Set properti form
            frmDashboard.TopLevel = False
            frmDashboard.FormBorderStyle = FormBorderStyle.None
            frmDashboard.Dock = DockStyle.Fill

            ' Tambahkan form ke panel
            TabPageHome.Controls.Add(frmDashboard)

            ' Tampilkan form

            frmDashboard.Show()
        ElseIf MaterialTabControl1.SelectedIndex = 1 Then
            Me.Text = "Master " & sMaster
        End If
    End Sub
#Region "Master"
    Private Sub fn_DisposeFormMaster()
        If Not frmItemList Is Nothing Then frmItemList.Dispose()
        frmItemList = Nothing

        If Not frmUOMList Is Nothing Then frmUOMList.Dispose()
        frmUOMList = Nothing

        If Not frmSignaList Is Nothing Then frmSignaList.Dispose()
        frmSignaList = Nothing

        If Not frmCaraPakaiList Is Nothing Then frmCaraPakaiList.Dispose()
        frmCaraPakaiList = Nothing
    End Sub
    Private Sub btnMasterObat_Click(sender As Object, e As EventArgs) Handles btnMasterObat.Click
        fn_DisposeFormMaster()
        sMaster = "Obat"
        sStok = True
        Me.Text = "Master " & sMaster

        PanelMaster.Dock = DockStyle.Fill

        PanelMaster.Controls.Clear()

        ' Set properti form

        frmItemList.TopLevel = False
        frmItemList.FormBorderStyle = FormBorderStyle.None
        frmItemList.Dock = DockStyle.Fill

        ' Tambahkan form ke panel
        PanelMaster.Controls.Add(frmItemList)

        ' Tampilkan form

        frmItemList.Show()
    End Sub
    Private Sub btnMasterSatuan_Click(sender As Object, e As EventArgs) Handles btnMasterSatuan.Click
        fn_DisposeFormMaster()
        sMaster = "Satuan"
        Me.Text = "Master " & sMaster

        PanelMaster.Dock = DockStyle.Fill

        PanelMaster.Controls.Clear()

        ' Set properti form
        frmUOMList.TopLevel = False
        frmUOMList.FormBorderStyle = FormBorderStyle.None
        frmUOMList.Dock = DockStyle.Fill

        ' Tambahkan form ke panel
        PanelMaster.Controls.Add(frmUOMList)

        ' Tampilkan form

        frmUOMList.Show()
    End Sub
    Private Sub btnMasterSigna_Click(sender As Object, e As EventArgs) Handles btnMasterSigna.Click
        fn_DisposeFormMaster()
        sMaster = "Signa"
        Me.Text = "Master " & sMaster

        PanelMaster.Dock = DockStyle.Fill

        PanelMaster.Controls.Clear()

        ' Set properti form
        frmSignaList.TopLevel = False
        frmSignaList.FormBorderStyle = FormBorderStyle.None
        frmSignaList.Dock = DockStyle.Fill

        ' Tambahkan form ke panel
        PanelMaster.Controls.Add(frmSignaList)

        ' Tampilkan form

        frmSignaList.Show()
    End Sub
    Private Sub btnMasterCaraPakai_Click(sender As Object, e As EventArgs) Handles btnMasterCaraPakai.Click
        fn_DisposeFormMaster()
        sMaster = "Cara Pakai"
        Me.Text = "Master " & sMaster

        PanelMaster.Dock = DockStyle.Fill

        PanelMaster.Controls.Clear()

        ' Set properti form
        frmCaraPakaiList.TopLevel = False
        frmCaraPakaiList.FormBorderStyle = FormBorderStyle.None
        frmCaraPakaiList.Dock = DockStyle.Fill

        ' Tambahkan form ke panel
        PanelMaster.Controls.Add(frmCaraPakaiList)

        ' Tampilkan form

        frmCaraPakaiList.Show()
    End Sub
#End Region

End Class
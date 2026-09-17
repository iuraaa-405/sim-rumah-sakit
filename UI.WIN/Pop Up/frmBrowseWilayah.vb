Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBrowseWilayah
    Private sLoad As Boolean = False

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sFind1 = ""
        sFind2 = ""
        fn_LoadKDPROPINSI()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Wilayah"

            'grv.Columns("MEMO").Caption = PaymentType.MEMO
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.Return And e.Shift = 0 Then
    '        cmdSelect_Click(sender, e)
    '    ElseIf e.KeyCode = Keys.Escape Then
    '        sFind1 = String.Empty
    '        sFind2 = String.Empty
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDKELURAHAN") Is Nothing Then
            Exit Sub
        End If

        sFind1 = grv.GetFocusedRowCellDisplayText(colKDKELURAHAN)
        sFind2 = grv.GetFocusedRowCellDisplayText(colMEMO)
        Me.Close()
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub fn_LoadKDPROPINSI()
        Dim oPropinsi As New Reference.clsPropinsi
        Try
            grdKDPROPINSI.Properties.DataSource = oPropinsi.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPROPINSI.Properties.ValueMember = "KDPROPINSI"
            grdKDPROPINSI.Properties.DisplayMember = "MEMO"

            grdKDPROPINSI.Text = oPropinsi.Default_Propinsi()

            If grdKDPROPINSI.Text <> "" Then
                fn_LoadKDKABUPATEN(grdKDPROPINSI.EditValue)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKABUPATEN(ByVal sKDPROPINSI As String)
        Try
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

            SQL = "EXEC "
            SQL &= "CARIKABUPATEN "
            SQL &= "@KDPROPINSI = '" & sKDPROPINSI & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARIKABUPATEN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDKABUPATEN.Properties.DataSource = ds.Tables("CARIKABUPATEN")
            grdKDKABUPATEN.Properties.ValueMember = "KDKABUPATEN"
            grdKDKABUPATEN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPROPINSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPROPINSI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDPROPINSI.Text <> "" Then
                If grdKDPROPINSI.Text <> "[EditValue is null]" Then
                    fn_LoadKDKABUPATEN(grdKDPROPINSI.EditValue)
                    grdKDKABUPATEN.ShowPopup()
                End If
            End If
        End If
    End Sub
    Private Sub fn_LoadKDKECAMATAN(ByVal sKDKABUPATEN As String)
        Try
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

            SQL = "EXEC "
            SQL &= "CARIKECAMATAN "
            SQL &= "@KDKABUTPATEN = '" & sKDKABUPATEN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARIKECAMATAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDKECAMATAN.Properties.DataSource = ds.Tables("CARIKECAMATAN")
            grdKDKECAMATAN.Properties.ValueMember = "KDKECAMATAN"
            grdKDKECAMATAN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKABUPATEN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKABUPATEN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDKABUPATEN.Text <> "" Then
                If grdKDKABUPATEN.Text <> "[EditValue is null]" Then
                    fn_LoadKDKECAMATAN(grdKDKABUPATEN.EditValue)
                    grdKDKECAMATAN.ShowPopup()
                End If
            End If
        End If
    End Sub
    Private Sub fn_LoadKDKELURAHAN(ByVal sKDKECAMATAN As String)
        Try
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

            SQL = "EXEC "
            SQL &= "CARIKELURAHAN "
            SQL &= "@KDKECAMATAN = '" & sKDKECAMATAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARIKELURAHAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("CARIKELURAHAN")
            grd.ForceInitialize()

            grv.Columns("KDKELURAHAN").VisibleIndex = -1
            grv.Columns("MEMO").Caption = "Kelurahan/Desa"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKECAMATAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKECAMATAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDKECAMATAN.Text <> "" Then
                If grdKDKECAMATAN.Text <> "[EditValue is null]" Then
                    fn_LoadKDKELURAHAN(grdKDKECAMATAN.EditValue)
                End If
            End If
        End If
    End Sub
End Class
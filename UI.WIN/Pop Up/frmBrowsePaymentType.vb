Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmBrowsePaymentType
    Private oType As Integer = 0

    Public Sub fn_LoadMe(ByVal FindType As Integer)
        oType = FindType
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PaymentType.TITLE

            grv.Columns("MEMO").Caption = PaymentType.MEMO
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return And e.Shift = 0 Then
            cmdSelect_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            sFind1 = String.Empty
            sFind2 = String.Empty
            Me.Close()
        End If
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        sFind1 = grv.GetFocusedRowCellDisplayText(colKDPAYMENTTYPE)
        sFind2 = grv.GetFocusedRowCellDisplayText(colMEMO)
        Me.Close()
    End Sub

    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub fn_LoadGrid()
        Dim oData As New Reference.clsPaymentType
        Try
            Try
                Dim ds = From x In oData.GetData _
                         Select x.KDPAYMENTTYPE, x.MEMO
                grd.DataSource = ds.ToList
                grd.RefreshDataSource()
            Catch ex As Exception
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class
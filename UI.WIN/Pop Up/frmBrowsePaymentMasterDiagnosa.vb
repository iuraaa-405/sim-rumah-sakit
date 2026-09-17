Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmBrowsePaymentMasterDiagnosa
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PaymentType.TITLE

            'grv.Columns("MEMO").Caption = PaymentType.MEMO
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
        sFind1 = grv.GetFocusedRowCellDisplayText("KDDIAGNOSAMASTER")
        'sFind2 = grv.GetFocusedRowCellDisplayText(colMEMO)
        Me.Close()
    End Sub

    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub fn_LoadGrid()
        Dim oData As New Reference.clsDiagnosaMasterPrice
        Try
            Try
                Dim ds = From x In oData.GetData
                         Select x.KDDIAGNOSAMASTER, x.JENIS, DIAGNOSA_1 = x.KDDIAGNOSA1 & x.M_DIAGNOSA.MEMO, DIAGNOSA_2 = x.KDDIAGNOSA2 & x.M_DIAGNOSA1.MEMO, DIAGNOSA_3 = x.KDDIAGNOSA3 & x.M_DIAGNOSA2.MEMO, DIAGNOSA_4 = x.KDDIAGNOSA4 & x.M_DIAGNOSA3.MEMO, DIAGNOSA_5 = x.KDDIAGNOSA5 & x.M_DIAGNOSA4.MEMO, DIAGNOSA_6 = x.KDDIAGNOSA6 & x.M_DIAGNOSA5.MEMO, x.DESCRIPTION, x.KLSI, x.KLS2, x.KLS3

                grd.DataSource = ds.ToList
                grd.RefreshDataSource()

                fn_LoadFormatData()

            Catch ex As Exception

            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grv.BestFitColumns()
    End Sub
End Class
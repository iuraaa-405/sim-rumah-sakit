Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmBrowseSN
    Private oType As Integer = 0
    Private sKDITEM As String = ""

    Public Sub fn_LoadMe(ByVal FindType As Integer, ByVal KDITEM As String)
        oType = FindType
        sKDITEM = KDITEM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
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
        sFind1 = grv.GetFocusedRowCellDisplayText("SERIALNUMBER").Replace(Environment.NewLine, "")
        Me.Close()
    End Sub

    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub fn_LoadGrid()
        Dim oPI As New Purchasing.clsPurchaseInvoice
        Dim oPR As New Purchasing.clsPurchaseReturn
        Dim oSI As New Sales.clsSalesInvoice
        Dim oSR As New Sales.clsSalesReturn

        Try
            Try
                Dim ds1 = (From x In oPI.GetDataDetail _
                         Where x.M_ITEM.ISACTIVE = True _
                         And x.KDITEM = sKDITEM _
                         Select x.KDITEM, SERIALNUMBER = x.REMARKS.Split(Environment.NewLine, 50, StringSplitOptions.RemoveEmptyEntries)).SelectMany(Function(x) x.SERIALNUMBER).ToList()

                Dim ds2 = (From x In oSI.GetDataDetail _
                         Where x.M_ITEM.ISACTIVE = True _
                         And x.KDITEM = sKDITEM _
                         Select x.KDITEM, SERIALNUMBER = x.REMARKS.Split(Environment.NewLine, 50, StringSplitOptions.RemoveEmptyEntries)).SelectMany(Function(x) x.SERIALNUMBER).ToList()

                Dim ds3 = (From x In oPR.GetDataDetail _
                         Where x.M_ITEM.ISACTIVE = True _
                         And x.KDITEM = sKDITEM _
                         Select x.KDITEM, SERIALNUMBER = x.REMARKS.Split(Environment.NewLine, 50, StringSplitOptions.RemoveEmptyEntries)).SelectMany(Function(x) x.SERIALNUMBER).ToList()

                Dim ds4 = (From x In oSR.GetDataDetail _
                         Where x.M_ITEM.ISACTIVE = True _
                         And x.KDITEM = sKDITEM _
                         Select x.KDITEM, SERIALNUMBER = x.REMARKS.Split(Environment.NewLine, 50, StringSplitOptions.RemoveEmptyEntries)).SelectMany(Function(x) x.SERIALNUMBER).ToList()

                Dim dsUnion = ds1.Except(ds2).Except(ds3).Union(ds4)

                Dim ds = From x In dsUnion _
                         Select KDITEM = sKDITEM, SERIALNUMBER = x

                grd.DataSource = ds.ToList
                grd.RefreshDataSource()
            Catch ex As Exception
            End Try
        Catch oErr As Exception
            MsgBox("Load Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class
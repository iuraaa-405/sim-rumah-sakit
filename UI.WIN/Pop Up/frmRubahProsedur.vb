Public Class frmRubahProsedur
    Private sNoid As String = ""

    Public Sub fn_loadDiagnosa(ByVal List As List(Of DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG), ByVal Noid As String)
        listprosedurkodeidRG = List
        sNoid = Noid
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGantiProsedur = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Sub LoadDiagnosa()
        grdCariDiagnosa.Properties.DataSource = ListProseduriDRG
        grdCariDiagnosa.Properties.ValueMember = "KDPROSEDUR"
        grdCariDiagnosa.Properties.DisplayMember = "MEMO"

        For Each xloop In listprosedurkodeidRG
            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()

            grvDiagnosa.SetFocusedRowCellValue(coljumlah, xloop.jumlah)
            grvDiagnosa.SetFocusedRowCellValue(colkdprpsedur, xloop.kdprpsedur)
            grvDiagnosa.SetFocusedRowCellValue(colmemo, xloop.memo.Trim)
            grvDiagnosa.UpdateCurrentRow()
        Next
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDiagnosa.DeleteSelectedRows()
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If btnOk.Text = "Ok" Then
            listprosedurkodeidRG.Clear()

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_PROSEDURIDRG
                dsRekap.datecreated = Now
                dsRekap.dateupdated = Now
                dsRekap.kodegrouper = sNoid
                dsRekap.seq = i
                dsRekap.jumlah = grvDiagnosa.GetRowCellValue(i, coljumlah)
                dsRekap.kdprpsedur = grvDiagnosa.GetRowCellValue(i, colkdprpsedur)
                dsRekap.memo = grvDiagnosa.GetRowCellValue(i, colmemo)
                listprosedurkodeidRG.Add(dsRekap)
            Next

            sGantiProsedur = True
            Me.Close()
        Else
            btnOk.Text = "Ok"
            lGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCari.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LoadDiagnosa()
        End If
    End Sub
    Private Sub grdCariDiagnosa_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiagnosa.EditValueChanged
        If grdCariDiagnosa.Text <> "" Then
            If grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT") Is Nothing Then
                Exit Sub
            End If

            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                sCek += 1
            Next

            If sCek = 0 Then
                If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISACTIVE")) = False Then
                    MsgBox("Prosedur " & grdCariDiagnosa.EditValue & " Tidak Bisa Jadi Primery", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT")) = False Then
                MsgBox("Prosedur " & grdCariDiagnosa.EditValue & " Tidak Valid Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()
            grvDiagnosa.SetFocusedRowCellValue(coljumlah, 0)
            grvDiagnosa.SetFocusedRowCellValue(colkdprpsedur, grdCariDiagnosa.EditValue)
            grvDiagnosa.SetFocusedRowCellValue(colmemo, grdCariDiagnosa.Text)
            grvDiagnosa.UpdateCurrentRow()
        End If
    End Sub
End Class
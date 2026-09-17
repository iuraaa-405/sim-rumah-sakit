Public Class frmRubahDiagnosa
    Private sNoid As String = ""

    Public Sub fn_loadDiagnosa(ByVal List As List(Of DataAccess.R_IDENTITAS_GROUPER_DATA_DIAGNOSAIDRG), ByVal Noid As String)
        listdiagnosakodeidRG = List
        sNoid = Noid
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGantiDiagnosa = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Sub LoadDiagnosa()
        grdCariDiagnosa.Properties.DataSource = ListDiagnosaiDRG
        grdCariDiagnosa.Properties.ValueMember = "KDDIAGNOSA"
        grdCariDiagnosa.Properties.DisplayMember = "MEMO"

        For Each xloop In listdiagnosakodeidRG
            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()

            grvDiagnosa.SetFocusedRowCellValue(colkategori, xloop.kategori.Trim)
            grvDiagnosa.SetFocusedRowCellValue(colkddiagnosa, xloop.kddiagnosa)
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
            listdiagnosakodeidRG.Clear()

            For i As Integer = 0 To grvDiagnosa.RowCount - 2
                Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_DATA_DIAGNOSAIDRG
                dsRekap.datecreated = Now
                dsRekap.dateupdated = Now
                dsRekap.kodegrouper = sNoid
                dsRekap.seq = i
                dsRekap.kategori = grvDiagnosa.GetRowCellValue(i, colkategori)
                dsRekap.kddiagnosa = grvDiagnosa.GetRowCellValue(i, colkddiagnosa)
                dsRekap.memo = IIf(dsRekap.kategori = "Primary", grvDiagnosa.GetRowCellValue(i, colmemo), "    " & grvDiagnosa.GetRowCellValue(i, colmemo))
                listdiagnosakodeidRG.Add(dsRekap)
            Next

            If listdiagnosakodeidRG.Count > 0 Then
                Dim sCek As Integer = 0

                For Each xloop In listdiagnosakodeidRG
                    If xloop.kategori = "Primary" Then
                        sCek += 1
                    End If
                Next

                If sCek <> 1 Then
                    MsgBox("Tidak Boleh ada Primary double", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    sGantiDiagnosa = True
                    Me.Close()
                End If
            Else
                MsgBox("Diagnosa Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
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
                    MsgBox("Diagnosa " & grdCariDiagnosa.EditValue & " Tidak Bisa Jadi Primery", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End If

            If CBool(grvCariDiagnosaiDRG.GetFocusedRowCellValue("ISDEFAULT")) = False Then
                MsgBox("Diagnosa " & grdCariDiagnosa.EditValue & " Tidak Valid Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            grvDiagnosa.Focus()
            grvDiagnosa.AddNewRow()
            grvDiagnosa.SetFocusedRowCellValue(colkategori, IIf(sCek = 0, "Primary", "Secondary"))
            grvDiagnosa.SetFocusedRowCellValue(colkddiagnosa, grdCariDiagnosa.EditValue)
            grvDiagnosa.SetFocusedRowCellValue(colmemo, grdCariDiagnosa.Text)
            grvDiagnosa.UpdateCurrentRow()
        End If
    End Sub
End Class
Public Class frmPersalinan
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sdelivery_method = String.Empty
        sdelivery_dttm = Now
        sletak_janin = String.Empty
        skondisi = String.Empty
        suse_manual = String.Empty
        suse_forcep = String.Empty
        suse_vacuum = String.Empty
        sshk_spesimen_ambil = String.Empty
        sshk_lokasi = String.Empty
        sshk_spesimen_dttm = Now
        sshk_alasan = String.Empty

        dedelivery_dttm.DateTime = Now
        deshk_spesimen_dttm.DateTime = Now

        lAlasan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub cboshk_spesimen_ambil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboshk_spesimen_ambil.SelectedIndexChanged
        If cboshk_spesimen_ambil.SelectedIndex = 0 Then
            lLokasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lWaktuPengambilan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lAlasan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lLokasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lWaktuPengambilan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lAlasan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        sdelivery_method = String.Empty
        sdelivery_dttm = Now
        sletak_janin = String.Empty
        skondisi = String.Empty
        suse_manual = String.Empty
        suse_forcep = String.Empty
        suse_vacuum = String.Empty
        sshk_spesimen_ambil = String.Empty
        sshk_lokasi = String.Empty
        sshk_spesimen_dttm = Now
        sshk_alasan = String.Empty
        Me.Close()
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        sdelivery_method = cbodelivery_method.Text
        sdelivery_dttm = dedelivery_dttm.DateTime
        sletak_janin = cboletak_janin.Text
        skondisi = cbokondisi.Text
        suse_manual = cbouse_manual.SelectedIndex
        suse_forcep = cbouse_forcep.SelectedIndex
        suse_vacuum = cbouse_vacuum.SelectedIndex
        sshk_spesimen_ambil = cboshk_spesimen_ambil.Text
        sshk_lokasi = cboshk_lokasi.Text
        sshk_spesimen_dttm = deshk_spesimen_dttm.DateTime
        sshk_alasan = cboshk_alasan.Text
        Me.Close()
    End Sub
End Class
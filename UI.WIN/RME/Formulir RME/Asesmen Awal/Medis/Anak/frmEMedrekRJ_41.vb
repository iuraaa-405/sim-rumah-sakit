Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_41
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_41 As New EMedrek.clsDigital_RJ_41
    Private down As Boolean = False
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS PASIEN TUMBUH KEMBANG"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_DOCTOR()

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
        txtKESATUAN.Properties.ReadOnly = Status
        txtTanggal.Properties.ReadOnly = Status
        txtAGAMA.Properties.ReadOnly = Status
        txtPANGKAT.Properties.ReadOnly = Status
        txtNRP.Properties.ReadOnly = Status
        txtBBTBLK.Properties.ReadOnly = Status
        txtDXMEDIS.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtWAKTU.Properties.ReadOnly = Status
        chkRUJUKAN_RS.Properties.ReadOnly = Status
        txtRUJUKAN_RS.Properties.ReadOnly = Status
        chkRUJUKAN_PUSKESMAS.Properties.ReadOnly = Status
        txtRUJUKAN_PUSKESMAS.Properties.ReadOnly = Status
        chkRUJUKAN_DR.Properties.ReadOnly = Status
        txtRUJUKAN_DR.Properties.ReadOnly = Status
        txtLAINNYA.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
        CheckEdit98.Properties.ReadOnly = Status
        CheckEdit99.Properties.ReadOnly = Status
        CheckEdit100.Properties.ReadOnly = Status
        CheckEdit101.Properties.ReadOnly = Status
        CheckEdit102.Properties.ReadOnly = Status
        CheckEdit103.Properties.ReadOnly = Status
        CheckEdit104.Properties.ReadOnly = Status
        CheckEdit105.Properties.ReadOnly = Status
        CheckEdit106.Properties.ReadOnly = Status
        CheckEdit107.Properties.ReadOnly = Status
        CheckEdit108.Properties.ReadOnly = Status
        CheckEdit109.Properties.ReadOnly = Status
        CheckEdit110.Properties.ReadOnly = Status
        CheckEdit111.Properties.ReadOnly = Status
        CheckEdit112.Properties.ReadOnly = Status
        CheckEdit113.Properties.ReadOnly = Status
        CheckEdit114.Properties.ReadOnly = Status
        CheckEdit115.Properties.ReadOnly = Status
        CheckEdit116.Properties.ReadOnly = Status
        CheckEdit117.Properties.ReadOnly = Status
        CheckEdit118.Properties.ReadOnly = Status
        CheckEdit119.Properties.ReadOnly = Status
        CheckEdit120.Properties.ReadOnly = Status
        CheckEdit121.Properties.ReadOnly = Status
        CheckEdit122.Properties.ReadOnly = Status
        CheckEdit123.Properties.ReadOnly = Status
        CheckEdit124.Properties.ReadOnly = Status
        CheckEdit125.Properties.ReadOnly = Status
        CheckEdit126.Properties.ReadOnly = Status
        CheckEdit127.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status

        txtPENDIDIKANORTU.Properties.ReadOnly = Status
        txtALAMAT.Properties.ReadOnly = Status
        txtSUKU.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit128.Properties.ReadOnly = Status
        CheckEdit129.Properties.ReadOnly = Status
        CheckEdit130.Properties.ReadOnly = Status
        CheckEdit131.Properties.ReadOnly = Status
        CheckEdit132.Properties.ReadOnly = Status
        CheckEdit133.Properties.ReadOnly = Status
        CheckEdit134.Properties.ReadOnly = Status
        CheckEdit135.Properties.ReadOnly = Status
        CheckEdit136.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '        sIsOtority = False
        '    End If
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        txtBBTBLK.ResetText()
        txtKEADAANUMUM.ResetText()
        chkRUJUKAN_RS.Checked = False
        txtRUJUKAN_RS.ResetText()
        chkRUJUKAN_PUSKESMAS.Checked = False
        txtRUJUKAN_PUSKESMAS.ResetText()
        chkRUJUKAN_DR.Checked = False
        txtRUJUKAN_DR.ResetText()
        txtLAINNYA.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit14.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        TextEdit6.ResetText()
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        MemoEdit7.ResetText()
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        TextEdit7.ResetText()
        CheckEdit56.Checked = False
        CheckEdit55.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        CheckEdit59.Checked = False
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        CheckEdit65.Checked = False
        CheckEdit67.Checked = False
        CheckEdit68.Checked = False
        CheckEdit66.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit77.Checked = False
        CheckEdit78.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        CheckEdit81.Checked = False
        CheckEdit82.Checked = False
        CheckEdit83.Checked = False
        CheckEdit84.Checked = False
        CheckEdit85.Checked = False
        CheckEdit86.Checked = False
        CheckEdit87.Checked = False
        CheckEdit88.Checked = False
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit92.Checked = False
        CheckEdit93.Checked = False
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
        CheckEdit98.Checked = False
        CheckEdit99.Checked = False
        CheckEdit100.Checked = False
        CheckEdit101.Checked = False
        CheckEdit102.Checked = False
        CheckEdit103.Checked = False
        CheckEdit104.Checked = False
        CheckEdit105.Checked = False
        CheckEdit106.Checked = False
        CheckEdit107.Checked = False
        CheckEdit108.Checked = False
        CheckEdit109.Checked = False
        CheckEdit110.Checked = False
        CheckEdit111.Checked = False
        CheckEdit112.Checked = False
        CheckEdit113.Checked = False
        CheckEdit114.Checked = False
        CheckEdit115.Checked = False
        CheckEdit116.Checked = False
        CheckEdit117.Checked = False
        CheckEdit118.Checked = False
        CheckEdit119.Checked = False
        CheckEdit120.Checked = False
        CheckEdit121.Checked = False
        CheckEdit122.Checked = False
        CheckEdit123.Checked = False
        CheckEdit124.Checked = False
        CheckEdit125.Checked = False
        CheckEdit126.Checked = False
        CheckEdit127.Checked = False
        TextEdit8.ResetText()
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        TextBox1.ResetText()
        TextBox2.Text = sUserID

        txtPENDIDIKANORTU.ResetText()
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit128.Checked = False
        CheckEdit129.Checked = False
        CheckEdit130.Checked = False
        CheckEdit131.Checked = False
        CheckEdit132.Checked = False
        CheckEdit133.Checked = False
        CheckEdit134.Checked = False
        CheckEdit135.Checked = False
        CheckEdit136.Checked = False
        TextEdit11.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()

        deDATE.DateTime = Now
        grdDOCTOR.TabIndex = sKDDOCTOR
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_41.GetData(sNoid)

            With ds
                grdDOCTOR.Text = .DOKTER_KODE
                txtBBTBLK.Text = .SDIGITALRJ41_1
                txtDXMEDIS.Text = .SDIGITALRJ41_2
                txtKEADAANUMUM.Text = .SDIGITALRJ41_3
                txtWAKTU.Text = .SDIGITALRJ41_4
                chkRUJUKAN_RS.Checked = .SDIGITALRJ41_5
                txtRUJUKAN_RS.Text = .SDIGITALRJ41_6
                chkRUJUKAN_PUSKESMAS.Checked = .SDIGITALRJ41_7
                txtRUJUKAN_PUSKESMAS.Text = .SDIGITALRJ41_8
                chkRUJUKAN_DR.Checked = .SDIGITALRJ41_9
                txtRUJUKAN_DR.Text = .SDIGITALRJ41_10
                txtLAINNYA.Text = .SDIGITALRJ41_11
                CheckEdit1.Checked = .SDIGITALRJ41_12
                CheckEdit2.Checked = .SDIGITALRJ41_13
                CheckEdit3.Checked = .SDIGITALRJ41_14
                CheckEdit4.Checked = .SDIGITALRJ41_15
                CheckEdit5.Checked = .SDIGITALRJ41_16
                CheckEdit6.Checked = .SDIGITALRJ41_17
                CheckEdit7.Checked = .SDIGITALRJ41_18
                CheckEdit8.Checked = .SDIGITALRJ41_19
                CheckEdit14.Checked = .SDIGITALRJ41_20
                CheckEdit12.Checked = .SDIGITALRJ41_21
                CheckEdit13.Checked = .SDIGITALRJ41_22
                CheckEdit15.Checked = .SDIGITALRJ41_23
                CheckEdit16.Checked = .SDIGITALRJ41_24
                TextEdit1.Text = .SDIGITALRJ41_25
                TextEdit2.Text = .SDIGITALRJ41_26
                TextEdit3.Text = .SDIGITALRJ41_27
                TextEdit4.Text = .SDIGITALRJ41_28
                TextEdit5.Text = .SDIGITALRJ41_29
                CheckEdit17.Checked = .SDIGITALRJ41_30
                CheckEdit18.Checked = .SDIGITALRJ41_31
                TextEdit6.Text = .SDIGITALRJ41_32
                CheckEdit19.Checked = .SDIGITALRJ41_33
                CheckEdit20.Checked = .SDIGITALRJ41_34
                CheckEdit21.Checked = .SDIGITALRJ41_35
                CheckEdit22.Checked = .SDIGITALRJ41_36
                CheckEdit23.Checked = .SDIGITALRJ41_37
                CheckEdit24.Checked = .SDIGITALRJ41_38
                CheckEdit25.Checked = .SDIGITALRJ41_39
                CheckEdit26.Checked = .SDIGITALRJ41_40
                CheckEdit27.Checked = .SDIGITALRJ41_41
                MemoEdit7.Text = .SDIGITALRJ41_42
                CheckEdit28.Checked = .SDIGITALRJ41_43
                CheckEdit29.Checked = .SDIGITALRJ41_44
                CheckEdit30.Checked = .SDIGITALRJ41_45
                CheckEdit31.Checked = .SDIGITALRJ41_46
                CheckEdit32.Checked = .SDIGITALRJ41_47
                CheckEdit33.Checked = .SDIGITALRJ41_48
                CheckEdit34.Checked = .SDIGITALRJ41_49
                CheckEdit35.Checked = .SDIGITALRJ41_50
                CheckEdit36.Checked = .SDIGITALRJ41_51
                CheckEdit37.Checked = .SDIGITALRJ41_52
                CheckEdit38.Checked = .SDIGITALRJ41_53
                CheckEdit39.Checked = .SDIGITALRJ41_54
                CheckEdit40.Checked = .SDIGITALRJ41_55
                CheckEdit41.Checked = .SDIGITALRJ41_56
                CheckEdit43.Checked = .SDIGITALRJ41_57
                CheckEdit44.Checked = .SDIGITALRJ41_58
                CheckEdit45.Checked = .SDIGITALRJ41_59
                CheckEdit46.Checked = .SDIGITALRJ41_60
                CheckEdit47.Checked = .SDIGITALRJ41_61
                CheckEdit48.Checked = .SDIGITALRJ41_62
                CheckEdit49.Checked = .SDIGITALRJ41_63
                CheckEdit50.Checked = .SDIGITALRJ41_64
                CheckEdit51.Checked = .SDIGITALRJ41_65
                CheckEdit52.Checked = .SDIGITALRJ41_66
                CheckEdit53.Checked = .SDIGITALRJ41_67
                CheckEdit54.Checked = .SDIGITALRJ41_68
                TextEdit7.Text = .SDIGITALRJ41_69
                CheckEdit56.Checked = .SDIGITALRJ41_70
                CheckEdit55.Checked = .SDIGITALRJ41_71
                CheckEdit57.Checked = .SDIGITALRJ41_72
                CheckEdit58.Checked = .SDIGITALRJ41_73
                CheckEdit59.Checked = .SDIGITALRJ41_74
                CheckEdit60.Checked = .SDIGITALRJ41_75
                CheckEdit61.Checked = .SDIGITALRJ41_76
                CheckEdit62.Checked = .SDIGITALRJ41_77
                CheckEdit63.Checked = .SDIGITALRJ41_78
                CheckEdit64.Checked = .SDIGITALRJ41_79
                CheckEdit65.Checked = .SDIGITALRJ41_80
                CheckEdit67.Checked = .SDIGITALRJ41_81
                CheckEdit68.Checked = .SDIGITALRJ41_82
                CheckEdit66.Checked = .SDIGITALRJ41_83
                CheckEdit69.Checked = .SDIGITALRJ41_84
                CheckEdit70.Checked = .SDIGITALRJ41_85
                CheckEdit71.Checked = .SDIGITALRJ41_86
                CheckEdit72.Checked = .SDIGITALRJ41_87
                CheckEdit73.Checked = .SDIGITALRJ41_88
                CheckEdit74.Checked = .SDIGITALRJ41_89
                CheckEdit75.Checked = .SDIGITALRJ41_90
                CheckEdit76.Checked = .SDIGITALRJ41_91
                CheckEdit77.Checked = .SDIGITALRJ41_92
                CheckEdit78.Checked = .SDIGITALRJ41_93
                CheckEdit79.Checked = .SDIGITALRJ41_94
                CheckEdit80.Checked = .SDIGITALRJ41_95
                CheckEdit81.Checked = .SDIGITALRJ41_96
                CheckEdit82.Checked = .SDIGITALRJ41_97
                CheckEdit83.Checked = .SDIGITALRJ41_98
                CheckEdit84.Checked = .SDIGITALRJ41_99
                CheckEdit85.Checked = .SDIGITALRJ41_100
                CheckEdit86.Checked = .SDIGITALRJ41_101
                CheckEdit87.Checked = .SDIGITALRJ41_102
                CheckEdit88.Checked = .SDIGITALRJ41_103
                CheckEdit89.Checked = .SDIGITALRJ41_104
                CheckEdit90.Checked = .SDIGITALRJ41_105
                CheckEdit91.Checked = .SDIGITALRJ41_106
                CheckEdit92.Checked = .SDIGITALRJ41_107
                CheckEdit93.Checked = .SDIGITALRJ41_108
                CheckEdit94.Checked = .SDIGITALRJ41_109
                CheckEdit95.Checked = .SDIGITALRJ41_110
                CheckEdit96.Checked = .SDIGITALRJ41_111
                CheckEdit97.Checked = .SDIGITALRJ41_112
                CheckEdit98.Checked = .SDIGITALRJ41_113
                CheckEdit99.Checked = .SDIGITALRJ41_114
                CheckEdit100.Checked = .SDIGITALRJ41_115
                CheckEdit101.Checked = .SDIGITALRJ41_116
                CheckEdit102.Checked = .SDIGITALRJ41_117
                CheckEdit103.Checked = .SDIGITALRJ41_118
                CheckEdit104.Checked = .SDIGITALRJ41_119
                CheckEdit105.Checked = .SDIGITALRJ41_120
                CheckEdit106.Checked = .SDIGITALRJ41_121
                CheckEdit107.Checked = .SDIGITALRJ41_122
                CheckEdit108.Checked = .SDIGITALRJ41_123
                CheckEdit109.Checked = .SDIGITALRJ41_124
                CheckEdit110.Checked = .SDIGITALRJ41_125
                CheckEdit111.Checked = .SDIGITALRJ41_126
                CheckEdit112.Checked = .SDIGITALRJ41_127
                CheckEdit113.Checked = .SDIGITALRJ41_128
                CheckEdit114.Checked = .SDIGITALRJ41_129
                CheckEdit115.Checked = .SDIGITALRJ41_130
                CheckEdit116.Checked = .SDIGITALRJ41_131
                CheckEdit117.Checked = .SDIGITALRJ41_132
                CheckEdit118.Checked = .SDIGITALRJ41_133
                CheckEdit119.Checked = .SDIGITALRJ41_134
                CheckEdit120.Checked = .SDIGITALRJ41_135
                CheckEdit121.Checked = .SDIGITALRJ41_136
                CheckEdit122.Checked = .SDIGITALRJ41_137
                CheckEdit123.Checked = .SDIGITALRJ41_138
                CheckEdit124.Checked = .SDIGITALRJ41_139
                CheckEdit125.Checked = .SDIGITALRJ41_140
                CheckEdit126.Checked = .SDIGITALRJ41_141
                CheckEdit127.Checked = .SDIGITALRJ41_142
                TextEdit8.Text = .SDIGITALRJ41_143
                MemoEdit1.Text = .SDIGITALRJ41_144
                MemoEdit2.Text = .SDIGITALRJ41_145
                MemoEdit3.Text = .SDIGITALRJ41_146
                MemoEdit4.Text = .SDIGITALRJ41_147
                MemoEdit5.Text = .SDIGITALRJ41_148
                MemoEdit6.Text = .SDIGITALRJ41_149
                TextBox1.Text = .SDIGITALRJ41_150
                TextBox2.Text = .SDIGITALRJ41_151

                txtPENDIDIKANORTU.Text = .SDIGITALRJ41_152
                txtALAMAT.Text = .SDIGITALRJ41_153
                txtSUKU.Text = .SDIGITALRJ41_154
                CheckEdit9.Checked = .SDIGITALRJ41_155
                CheckEdit10.Checked = .SDIGITALRJ41_156
                CheckEdit11.Checked = .SDIGITALRJ41_157
                CheckEdit128.Checked = .SDIGITALRJ41_158
                CheckEdit129.Checked = .SDIGITALRJ41_159
                CheckEdit130.Checked = .SDIGITALRJ41_160
                CheckEdit131.Checked = .SDIGITALRJ41_161
                CheckEdit132.Checked = .SDIGITALRJ41_162
                CheckEdit133.Checked = .SDIGITALRJ41_163
                CheckEdit134.Checked = .SDIGITALRJ41_164
                CheckEdit135.Checked = .SDIGITALRJ41_165
                CheckEdit136.Checked = .SDIGITALRJ41_166
                TextEdit11.Text = .SDIGITALRJ41_167
                TextEdit9.Text = .SDIGITALRJ41_168
                TextEdit10.Text = .SDIGITALRJ41_169
                TextEdit12.Text = .SDIGITALRJ41_170
                TextEdit13.Text = .SDIGITALRJ41_171
                TextEdit14.Text = .SDIGITALRJ41_172
                TextEdit15.Text = .SDIGITALRJ41_173

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New EMedrek.clsDigital_RJ_03
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            'bb
            TextEdit3.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            'tb
            TextEdit4.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
        Else
            'MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.Focus()
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
            Dim ds = oS_DIGITAL_RJ_41.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                Try
                    .DATECREATED = oS_DIGITAL_RJ_41.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .SDIGITALRJ41_1 = txtBBTBLK.Text
                .SDIGITALRJ41_2 = txtDXMEDIS.Text
                .SDIGITALRJ41_3 = txtKEADAANUMUM.Text
                .SDIGITALRJ41_4 = txtWAKTU.Text
                .SDIGITALRJ41_5 = chkRUJUKAN_RS.Checked
                .SDIGITALRJ41_6 = txtRUJUKAN_RS.Text
                .SDIGITALRJ41_7 = chkRUJUKAN_PUSKESMAS.Checked
                .SDIGITALRJ41_8 = txtRUJUKAN_PUSKESMAS.Text
                .SDIGITALRJ41_9 = chkRUJUKAN_DR.Checked
                .SDIGITALRJ41_10 = txtRUJUKAN_DR.Text
                .SDIGITALRJ41_11 = txtLAINNYA.Text
                .SDIGITALRJ41_12 = CheckEdit1.Checked
                .SDIGITALRJ41_13 = CheckEdit2.Checked
                .SDIGITALRJ41_14 = CheckEdit3.Checked
                .SDIGITALRJ41_15 = CheckEdit4.Checked
                .SDIGITALRJ41_16 = CheckEdit5.Checked
                .SDIGITALRJ41_17 = CheckEdit6.Checked
                .SDIGITALRJ41_18 = CheckEdit7.Checked
                .SDIGITALRJ41_19 = CheckEdit8.Checked
                .SDIGITALRJ41_20 = CheckEdit14.Checked
                .SDIGITALRJ41_21 = CheckEdit12.Checked
                .SDIGITALRJ41_22 = CheckEdit13.Checked
                .SDIGITALRJ41_23 = CheckEdit15.Checked
                .SDIGITALRJ41_24 = CheckEdit16.Checked
                .SDIGITALRJ41_25 = TextEdit1.Text
                .SDIGITALRJ41_26 = TextEdit2.Text
                .SDIGITALRJ41_27 = TextEdit3.Text
                .SDIGITALRJ41_28 = TextEdit4.Text
                .SDIGITALRJ41_29 = TextEdit5.Text
                .SDIGITALRJ41_30 = CheckEdit17.Checked
                .SDIGITALRJ41_31 = CheckEdit18.Checked
                .SDIGITALRJ41_32 = TextEdit6.Text
                .SDIGITALRJ41_33 = CheckEdit19.Checked
                .SDIGITALRJ41_34 = CheckEdit20.Checked
                .SDIGITALRJ41_35 = CheckEdit21.Checked
                .SDIGITALRJ41_36 = CheckEdit22.Checked
                .SDIGITALRJ41_37 = CheckEdit23.Checked
                .SDIGITALRJ41_38 = CheckEdit24.Checked
                .SDIGITALRJ41_39 = CheckEdit25.Checked
                .SDIGITALRJ41_40 = CheckEdit26.Checked
                .SDIGITALRJ41_41 = CheckEdit27.Checked
                .SDIGITALRJ41_42 = MemoEdit7.Text
                .SDIGITALRJ41_43 = CheckEdit28.Checked
                .SDIGITALRJ41_44 = CheckEdit29.Checked
                .SDIGITALRJ41_45 = CheckEdit30.Checked
                .SDIGITALRJ41_46 = CheckEdit31.Checked
                .SDIGITALRJ41_47 = CheckEdit32.Checked
                .SDIGITALRJ41_48 = CheckEdit33.Checked
                .SDIGITALRJ41_49 = CheckEdit34.Checked
                .SDIGITALRJ41_50 = CheckEdit35.Checked
                .SDIGITALRJ41_51 = CheckEdit36.Checked
                .SDIGITALRJ41_52 = CheckEdit37.Checked
                .SDIGITALRJ41_53 = CheckEdit38.Checked
                .SDIGITALRJ41_54 = CheckEdit39.Checked
                .SDIGITALRJ41_55 = CheckEdit40.Checked
                .SDIGITALRJ41_56 = CheckEdit41.Checked
                .SDIGITALRJ41_57 = CheckEdit43.Checked
                .SDIGITALRJ41_58 = CheckEdit44.Checked
                .SDIGITALRJ41_59 = CheckEdit45.Checked
                .SDIGITALRJ41_60 = CheckEdit46.Checked
                .SDIGITALRJ41_61 = CheckEdit47.Checked
                .SDIGITALRJ41_62 = CheckEdit48.Checked
                .SDIGITALRJ41_63 = CheckEdit49.Checked
                .SDIGITALRJ41_64 = CheckEdit50.Checked
                .SDIGITALRJ41_65 = CheckEdit51.Checked
                .SDIGITALRJ41_66 = CheckEdit52.Checked
                .SDIGITALRJ41_67 = CheckEdit53.Checked
                .SDIGITALRJ41_68 = CheckEdit54.Checked
                .SDIGITALRJ41_69 = TextEdit7.Text
                .SDIGITALRJ41_70 = CheckEdit56.Checked
                .SDIGITALRJ41_71 = CheckEdit55.Checked
                .SDIGITALRJ41_72 = CheckEdit57.Checked
                .SDIGITALRJ41_73 = CheckEdit58.Checked
                .SDIGITALRJ41_74 = CheckEdit59.Checked
                .SDIGITALRJ41_75 = CheckEdit60.Checked
                .SDIGITALRJ41_76 = CheckEdit61.Checked
                .SDIGITALRJ41_77 = CheckEdit62.Checked
                .SDIGITALRJ41_78 = CheckEdit63.Checked
                .SDIGITALRJ41_79 = CheckEdit64.Checked
                .SDIGITALRJ41_80 = CheckEdit65.Checked
                .SDIGITALRJ41_81 = CheckEdit67.Checked
                .SDIGITALRJ41_82 = CheckEdit68.Checked
                .SDIGITALRJ41_83 = CheckEdit66.Checked
                .SDIGITALRJ41_84 = CheckEdit69.Checked
                .SDIGITALRJ41_85 = CheckEdit70.Checked
                .SDIGITALRJ41_86 = CheckEdit71.Checked
                .SDIGITALRJ41_87 = CheckEdit72.Checked
                .SDIGITALRJ41_88 = CheckEdit73.Checked
                .SDIGITALRJ41_89 = CheckEdit74.Checked
                .SDIGITALRJ41_90 = CheckEdit75.Checked
                .SDIGITALRJ41_91 = CheckEdit76.Checked
                .SDIGITALRJ41_92 = CheckEdit77.Checked
                .SDIGITALRJ41_93 = CheckEdit78.Checked
                .SDIGITALRJ41_94 = CheckEdit79.Checked
                .SDIGITALRJ41_95 = CheckEdit80.Checked
                .SDIGITALRJ41_96 = CheckEdit81.Checked
                .SDIGITALRJ41_97 = CheckEdit82.Checked
                .SDIGITALRJ41_98 = CheckEdit83.Checked
                .SDIGITALRJ41_99 = CheckEdit84.Checked
                .SDIGITALRJ41_100 = CheckEdit85.Checked
                .SDIGITALRJ41_101 = CheckEdit86.Checked
                .SDIGITALRJ41_102 = CheckEdit87.Checked
                .SDIGITALRJ41_103 = CheckEdit88.Checked
                .SDIGITALRJ41_104 = CheckEdit89.Checked
                .SDIGITALRJ41_105 = CheckEdit90.Checked
                .SDIGITALRJ41_106 = CheckEdit91.Checked
                .SDIGITALRJ41_107 = CheckEdit92.Checked
                .SDIGITALRJ41_108 = CheckEdit93.Checked
                .SDIGITALRJ41_109 = CheckEdit94.Checked
                .SDIGITALRJ41_110 = CheckEdit95.Checked
                .SDIGITALRJ41_111 = CheckEdit96.Checked
                .SDIGITALRJ41_112 = CheckEdit97.Checked
                .SDIGITALRJ41_113 = CheckEdit98.Checked
                .SDIGITALRJ41_114 = CheckEdit99.Checked
                .SDIGITALRJ41_115 = CheckEdit100.Checked
                .SDIGITALRJ41_116 = CheckEdit101.Checked
                .SDIGITALRJ41_117 = CheckEdit102.Checked
                .SDIGITALRJ41_118 = CheckEdit103.Checked
                .SDIGITALRJ41_119 = CheckEdit104.Checked
                .SDIGITALRJ41_120 = CheckEdit105.Checked
                .SDIGITALRJ41_121 = CheckEdit106.Checked
                .SDIGITALRJ41_122 = CheckEdit107.Checked
                .SDIGITALRJ41_123 = CheckEdit108.Checked
                .SDIGITALRJ41_124 = CheckEdit109.Checked
                .SDIGITALRJ41_125 = CheckEdit110.Checked
                .SDIGITALRJ41_126 = CheckEdit111.Checked
                .SDIGITALRJ41_127 = CheckEdit112.Checked
                .SDIGITALRJ41_128 = CheckEdit113.Checked
                .SDIGITALRJ41_129 = CheckEdit114.Checked
                .SDIGITALRJ41_130 = CheckEdit115.Checked
                .SDIGITALRJ41_131 = CheckEdit116.Checked
                .SDIGITALRJ41_132 = CheckEdit117.Checked
                .SDIGITALRJ41_133 = CheckEdit118.Checked
                .SDIGITALRJ41_134 = CheckEdit119.Checked
                .SDIGITALRJ41_135 = CheckEdit120.Checked
                .SDIGITALRJ41_136 = CheckEdit121.Checked
                .SDIGITALRJ41_137 = CheckEdit122.Checked
                .SDIGITALRJ41_138 = CheckEdit123.Checked
                .SDIGITALRJ41_139 = CheckEdit124.Checked
                .SDIGITALRJ41_140 = CheckEdit125.Checked
                .SDIGITALRJ41_141 = CheckEdit126.Checked
                .SDIGITALRJ41_142 = CheckEdit127.Checked
                .SDIGITALRJ41_143 = TextEdit8.Text
                .SDIGITALRJ41_144 = MemoEdit1.Text
                .SDIGITALRJ41_145 = MemoEdit2.Text
                .SDIGITALRJ41_146 = MemoEdit3.Text
                .SDIGITALRJ41_147 = MemoEdit4.Text
                .SDIGITALRJ41_148 = MemoEdit5.Text
                .SDIGITALRJ41_149 = MemoEdit6.Text
                .SDIGITALRJ41_150 = TextBox1.Text
                .SDIGITALRJ41_151 = TextBox2.Text

                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                Try
                    .CETAK = oS_DIGITAL_RJ_41.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""

                .SDIGITALRJ41_152 = txtPENDIDIKANORTU.Text
                .SDIGITALRJ41_153 = txtALAMAT.Text
                .SDIGITALRJ41_154 = txtSUKU.Text
                .SDIGITALRJ41_155 = CheckEdit9.Checked
                .SDIGITALRJ41_156 = CheckEdit10.Checked
                .SDIGITALRJ41_157 = CheckEdit11.Checked
                .SDIGITALRJ41_158 = CheckEdit128.Checked
                .SDIGITALRJ41_159 = CheckEdit129.Checked
                .SDIGITALRJ41_160 = CheckEdit130.Checked
                .SDIGITALRJ41_161 = CheckEdit131.Checked
                .SDIGITALRJ41_162 = CheckEdit132.Checked
                .SDIGITALRJ41_163 = CheckEdit133.Checked
                .SDIGITALRJ41_164 = CheckEdit134.Checked
                .SDIGITALRJ41_165 = CheckEdit135.Checked
                .SDIGITALRJ41_166 = CheckEdit136.Checked
                .SDIGITALRJ41_167 = TextEdit11.Text
                .SDIGITALRJ41_168 = TextEdit9.Text
                .SDIGITALRJ41_169 = TextEdit10.Text
                .SDIGITALRJ41_170 = TextEdit12.Text
                .SDIGITALRJ41_171 = TextEdit13.Text
                .SDIGITALRJ41_172 = TextEdit14.Text
                .SDIGITALRJ41_173 = TextEdit15.Text

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_41.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_41.UpdateData(ds)
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
            'Case Keys.F12
            '    btnClose_Click()
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
            'Case Keys.F5
            '    If btnSaveNew.Enabled = True Then
            '        btnReload_Click()
            '    End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    'Private Sub btnReload_Click() Handles btnReload.ItemClick
    '    Dim dsKunjungan = oS_DIGITAL_RJ_41.GetDataByKunjungan(sNoId)

    '    Dim listPenunjang As New List(Of String)
    '    Dim listTindakanPengobatan As New List(Of String)

    '    If dsKunjungan IsNot Nothing Then
    '        Dim oOrderTindakan As New Inventory.clsOrderTindakan
    '        Dim oOrderLab As New Inventory.clsOrderLab
    '        Dim oOrderRad As New Inventory.clsOrderRad
    '        Dim oKonsul As New Digital.clsKonsul
    '        Dim oKonsulJawab As New Digital.clsJawabKonsul

    '        Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                         Join y In oOrderTindakan.GetDataDetail()
    '                         On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
    '                         Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderLab.GetDataDetail()
    '                    On x.KDORDERLAB Equals y.KDORDERLAB
    '                    Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderRad.GetDataDetail()
    '                    On x.KDORDERRAD Equals y.KDORDERRAD
    '                    Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                       Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                            Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsUnionPenunjang = dsLab.Union(dsRad)

    '        For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
    '            listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

    '        For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
    '            listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim oResep As New Inventory.clsOrderResep

    '        Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

    '        For Each xloop In dsResep
    '            For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
    '                listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
    '            Next
    '        Next

    '        'MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
    '        'MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_DOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
    '    TextEdit14.Text = "dalam batas normal"
    '    TextEdit19.Text = "dalam batas normal"
    '    TextEdit18.Text = "dalam batas normal"
    '    TextEdit17.Text = "dalam batas normal"
    '    TextEdit16.Text = "dalam batas normal"
    '    TextEdit15.Text = "dalam batas normal"
    '    TextEdit20.Text = "dalam batas normal"
    '    TextEdit21.Text = "dalam batas normal"
    '    TextEdit22.Text = "dalam batas normal"
    'End Sub

    Private Sub frmEMedrekRJ_41_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class
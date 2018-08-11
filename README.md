# payroll-absensi-vbnet

program penggajian/ payroll dengan acuan absensi dari mesin absensi merk attendance,

alur dari program ini adalah :
pegawai absen dimesin absensi -------- program menarik data absen pegawai --------- data diolah untuk menghitung lembur pegawai, dan data kehadiran

program dibuat menggunakan api yang telah disediakan oleh pihak attendace, waktu untuk download data absensi pegawai tergantung dari admin yang menggunakan,
biasanya ditarik setiap hari untuk menghitung uang lembur dan mengetahui karyawan masuk atau tidak

fitur program ini adalah :
1. otomatis download dari mesin absen (tanpa input ulang)
2. hitung otomatis uang lembur pegawai
3. otomatis mengetahui jam keluar dan masuk pegawai sesuai dengan shift nya(terbagi menjadi 3 shift)
4. penggajian bulanan,mingguan dan harian
5. input hasil borongan berdasarkan jam masuk dan keluar pegawai


bahasa pemrograman yang dipakai adalah vb.net, dan sql server untuk databasenya

team untuk membuat program : 1 orang

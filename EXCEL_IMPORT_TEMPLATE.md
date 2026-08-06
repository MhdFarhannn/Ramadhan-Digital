# Template Excel untuk Registrasi Bulk User

Untuk menggunakan fitur registrasi bulk via Excel, silakan ikuti format berikut:

## Struktur File Excel

File Excel harus memiliki kolom-kolom berikut (dalam urutan ini):

| Kolom | Nama | Tipe | Keterangan |
|-------|------|------|-----------|
| A | Nama | Text | Nama lengkap user (Wajib) |
| B | Username | Text | Username untuk login (Wajib, harus unik) |
| C | Password | Text | Password awal (Wajib) |

**Catatan Penting:**
- **IdRole**: Sudah di-hardcode sesuai dengan endpoint yang digunakan:
  - Untuk siswa: IdRole = 3 (Santri)
  - Untuk guru: IdRole = 2 (Guru)
- **IdKelas**: Untuk siswa, akan dipilih melalui dropdown di frontend, BUKAN dari Excel

## Contoh Data untuk Siswa

```
Nama                 | Username      | Password
==================================================
Ahmad Bin Abdillah   | ahmad123      | pass123456
Siti Khadijah        | siti456       | pass123456
Muhammad Ali         | muhammadali   | pass123456
Fatima Azzahra       | fatima789     | pass123456
```

## Contoh Data untuk Guru

```
Nama                  | Username      | Password
=================================================
Dr. Abdullah Jamal    | dr.abdullah   | pass123456
Ibu Nurul Fitrah      | nurul.fitrah  | pass123456
```

## Catatan Penting

1. **Header Row**: Baris pertama adalah header (Nama, Username, Password)
2. **Username Unik**: Setiap username harus unik, jika sudah terdaftar akan dilewatkan
3. **Password**: Password akan di-hash secara otomatis sebelum disimpan
4. **Format File**: Harus berformat .xlsx atau .xls
5. **Encoding**: Pastikan file menggunakan encoding UTF-8 untuk karakter khusus
6. **Kolom IdRole dan IdKelas**: Tidak perlu ada di Excel, sudah hardcode dan dari frontend

## Endpoint API

### Registrasi Siswa (IdRole = 3)

**POST** `/api/v1/auth/register-bulk-excel-siswa`

#### Request
- Header: `Authorization: Bearer {token}`
- Query Parameter: `idKelas={id_kelas}` (dari dropdown frontend)
- Body: Form-Data dengan key `file` berisi file Excel

#### Response
```json
{
  "success": true,
  "importedCount": 3,
  "errors": [],
  "message": "Berhasil mengimpor 3 siswa ke kelas 1"
}
```

#### Contoh Curl
```bash
curl -X POST "https://localhost:5001/api/v1/auth/register-bulk-excel-siswa?idKelas=1" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -F "file=@siswa.xlsx"
```

### Registrasi Guru (IdRole = 2)

**POST** `/api/v1/auth/register-bulk-excel-guru`

#### Request
- Header: `Authorization: Bearer {token}`
- Body: Form-Data dengan key `file` berisi file Excel

#### Response
```json
{
  "success": true,
  "importedCount": 2,
  "errors": [],
  "message": "Berhasil mengimpor 2 guru"
}
```

#### Contoh Curl
```bash
curl -X POST "https://localhost:5001/api/v1/auth/register-bulk-excel-guru" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -F "file=@guru.xlsx"
```

## Troubleshooting

**Error: "Username '...' sudah terdaftar"**
- Username tersebut sudah ada di database, gunakan username yang berbeda

**Error: "Kolom 'Nama' tidak boleh kosong"**
- Kolom Nama harus diisi untuk setiap baris

**Error: "Kolom 'Username' tidak boleh kosong"**
- Kolom Username harus diisi untuk setiap baris

**Error: "Kolom 'Password' tidak boleh kosong"**
- Kolom Password harus diisi untuk setiap baris

**Error: "Format file harus .xlsx atau .xls"**
- Pastikan file yang diupload adalah file Excel dengan ekstensi .xlsx atau .xls

**Error: "File tidak ditemukan atau kosong"**
- Pastikan file ada dan memiliki data (minimal 1 baris data selain header)


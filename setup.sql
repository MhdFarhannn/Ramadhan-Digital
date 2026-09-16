--
-- PostgreSQL database dump (MERGED: full schema from setup.sql + seed data from ramadhan_schema.sql)
--


-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;


--
-- Schema (all tables, sequences) — from setup.sql
--

--
-- Name: absensi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.absensi (
    id integer NOT NULL,
    id_user integer NOT NULL,
    tanggal date NOT NULL,
    id_status_absensi integer NOT NULL
);


ALTER TABLE public.absensi OWNER TO postgres;

--
-- Name: absensi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.absensi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.absensi_id_seq OWNER TO postgres;

--
-- Name: absensi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.absensi_id_seq OWNED BY public.absensi.id;


--
-- Name: ayat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ayat (
    id integer NOT NULL,
    id_surah integer NOT NULL,
    nomor integer NOT NULL,
    arab text NOT NULL,
    terjemah text NOT NULL
);


ALTER TABLE public.ayat OWNER TO postgres;

--
-- Name: ayat_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ayat_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ayat_id_seq OWNER TO postgres;

--
-- Name: ayat_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ayat_id_seq OWNED BY public.ayat.id;


--
-- Name: bacaan_sholat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bacaan_sholat (
    id integer NOT NULL,
    id_hukum integer NOT NULL,
    urutan integer,
    nama character varying(100),
    gerakan character varying(100),
    arabic text,
    translate text
);


ALTER TABLE public.bacaan_sholat OWNER TO postgres;

--
-- Name: bacaan_sholat_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bacaan_sholat_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.bacaan_sholat_id_seq OWNER TO postgres;

--
-- Name: bacaan_sholat_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bacaan_sholat_id_seq OWNED BY public.bacaan_sholat.id;


--
-- Name: detail_sholat_wajib; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.detail_sholat_wajib (
    id integer NOT NULL,
    id_ibadah_harian integer NOT NULL,
    id_kategori_sholat_wajib integer NOT NULL,
    id_status_sholat_wajib integer NOT NULL
);


ALTER TABLE public.detail_sholat_wajib OWNER TO postgres;

--
-- Name: detail_sholat_wajib_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.detail_sholat_wajib_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.detail_sholat_wajib_id_seq OWNER TO postgres;

--
-- Name: detail_sholat_wajib_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.detail_sholat_wajib_id_seq OWNED BY public.detail_sholat_wajib.id;


--
-- Name: dzikir_setelah_sholat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.dzikir_setelah_sholat (
    id integer NOT NULL,
    nama character varying(150),
    arabic text,
    terjemah text,
    sumber character varying(150)
);


ALTER TABLE public.dzikir_setelah_sholat OWNER TO postgres;

--
-- Name: dzikir_setelah_sholat_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.dzikir_setelah_sholat_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.dzikir_setelah_sholat_id_seq OWNER TO postgres;

--
-- Name: dzikir_setelah_sholat_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.dzikir_setelah_sholat_id_seq OWNED BY public.dzikir_setelah_sholat.id;


--
-- Name: hukum; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hukum (
    id integer NOT NULL,
    nama character varying(20) NOT NULL
);


ALTER TABLE public.hukum OWNER TO postgres;

--
-- Name: hukum_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hukum_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.hukum_id_seq OWNER TO postgres;

--
-- Name: hukum_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.hukum_id_seq OWNED BY public.hukum.id;


--
-- Name: ibadah_harian; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ibadah_harian (
    id integer NOT NULL,
    id_user integer NOT NULL,
    tanggal date NOT NULL,
    membaca_alquran boolean DEFAULT false,
    target_bacaan character varying(100)
);


ALTER TABLE public.ibadah_harian OWNER TO postgres;

--
-- Name: ibadah_harian_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ibadah_harian_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ibadah_harian_id_seq OWNER TO postgres;

--
-- Name: ibadah_harian_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ibadah_harian_id_seq OWNED BY public.ibadah_harian.id;


--
-- Name: ibadah_sunnah; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ibadah_sunnah (
    id integer NOT NULL,
    id_kategori_sunnah integer NOT NULL,
    id_user integer NOT NULL,
    tanggal date NOT NULL
);


ALTER TABLE public.ibadah_sunnah OWNER TO postgres;

--
-- Name: ibadah_sunnah_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ibadah_sunnah_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ibadah_sunnah_id_seq OWNER TO postgres;

--
-- Name: ibadah_sunnah_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ibadah_sunnah_id_seq OWNED BY public.ibadah_sunnah.id;


--
-- Name: kategori_sholat_wajib; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kategori_sholat_wajib (
    id integer NOT NULL,
    nama character varying(30) NOT NULL
);


ALTER TABLE public.kategori_sholat_wajib OWNER TO postgres;

--
-- Name: kategori_sholat_wajib_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kategori_sholat_wajib_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kategori_sholat_wajib_id_seq OWNER TO postgres;

--
-- Name: kategori_sholat_wajib_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kategori_sholat_wajib_id_seq OWNED BY public.kategori_sholat_wajib.id;


--
-- Name: kategori_sunnah; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kategori_sunnah (
    id integer NOT NULL,
    nama character varying(50) NOT NULL
);


ALTER TABLE public.kategori_sunnah OWNER TO postgres;

--
-- Name: kategori_sunnah_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kategori_sunnah_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kategori_sunnah_id_seq OWNER TO postgres;

--
-- Name: kategori_sunnah_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kategori_sunnah_id_seq OWNED BY public.kategori_sunnah.id;


--
-- Name: kegiatan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kegiatan (
    id integer NOT NULL,
    judul character varying(150) NOT NULL,
    pemateri character varying(100),
    tanggal date NOT NULL
);


ALTER TABLE public.kegiatan OWNER TO postgres;

--
-- Name: kegiatan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kegiatan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kegiatan_id_seq OWNER TO postgres;

--
-- Name: kegiatan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kegiatan_id_seq OWNED BY public.kegiatan.id;


--
-- Name: kegiatan_user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kegiatan_user (
    id integer NOT NULL,
    id_user integer NOT NULL,
    id_kegiatan integer NOT NULL,
    note text
);


ALTER TABLE public.kegiatan_user OWNER TO postgres;

--
-- Name: kegiatan_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kegiatan_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kegiatan_user_id_seq OWNER TO postgres;

--
-- Name: kegiatan_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kegiatan_user_id_seq OWNED BY public.kegiatan_user.id;


--
-- Name: kelas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kelas (
    id integer NOT NULL,
    nama character varying(50) NOT NULL,
    angkatan character varying(20) NOT NULL
);


ALTER TABLE public.kelas OWNER TO postgres;

--
-- Name: kelas_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kelas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kelas_id_seq OWNER TO postgres;

--
-- Name: kelas_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kelas_id_seq OWNED BY public.kelas.id;


--
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.role OWNER TO postgres;

--
-- Name: role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.role_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.role_id_seq OWNER TO postgres;

--
-- Name: role_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.role_id_seq OWNED BY public.role.id;


--
-- Name: setoran_hafalan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.setoran_hafalan (
    id integer NOT NULL,
    id_user integer NOT NULL,
    id_surah integer,
    id_bacaan_sholat integer,
    id_status_setoran_hafalan integer NOT NULL,
    note text,
    tanggal_setoran date NOT NULL,
    id_kelas integer
);


ALTER TABLE public.setoran_hafalan OWNER TO postgres;

--
-- Name: setoran_hafalan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.setoran_hafalan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.setoran_hafalan_id_seq OWNER TO postgres;

--
-- Name: setoran_hafalan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.setoran_hafalan_id_seq OWNED BY public.setoran_hafalan.id;


--
-- Name: status_absensi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.status_absensi (
    id integer NOT NULL,
    nama character varying(30) NOT NULL
);


ALTER TABLE public.status_absensi OWNER TO postgres;

--
-- Name: status_absensi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.status_absensi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.status_absensi_id_seq OWNER TO postgres;

--
-- Name: status_absensi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.status_absensi_id_seq OWNED BY public.status_absensi.id;


--
-- Name: status_setoran_hafalan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.status_setoran_hafalan (
    id integer NOT NULL,
    nama character varying(50) NOT NULL
);


ALTER TABLE public.status_setoran_hafalan OWNER TO postgres;

--
-- Name: status_setoran_hafalan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.status_setoran_hafalan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.status_setoran_hafalan_id_seq OWNER TO postgres;

--
-- Name: status_setoran_hafalan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.status_setoran_hafalan_id_seq OWNED BY public.status_setoran_hafalan.id;


--
-- Name: status_sholat_wajib; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.status_sholat_wajib (
    id integer NOT NULL,
    nama character varying(30) NOT NULL
);


ALTER TABLE public.status_sholat_wajib OWNER TO postgres;

--
-- Name: status_sholat_wajib_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.status_sholat_wajib_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.status_sholat_wajib_id_seq OWNER TO postgres;

--
-- Name: status_sholat_wajib_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.status_sholat_wajib_id_seq OWNED BY public.status_sholat_wajib.id;


--
-- Name: surah; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.surah (
    id integer NOT NULL,
    surah character varying(100) NOT NULL,
    artisurat character varying(100),
    tempat_turun character varying(30),
    nomor integer NOT NULL
);


ALTER TABLE public.surah OWNER TO postgres;

--
-- Name: surah_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.surah_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.surah_id_seq OWNER TO postgres;

--
-- Name: surah_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.surah_id_seq OWNED BY public.surah.id;


--
-- Name: tausiah; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tausiah (
    id integer NOT NULL,
    id_user integer NOT NULL,
    tanggal date NOT NULL,
    judul_tausiah character varying(150),
    nama_penceramah character varying(100),
    ringkasan text
);


ALTER TABLE public.tausiah OWNER TO postgres;

--
-- Name: tausiah_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tausiah_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tausiah_id_seq OWNER TO postgres;

--
-- Name: tausiah_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tausiah_id_seq OWNED BY public.tausiah.id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    id_role integer NOT NULL,
    id_kelas integer,
    nama character varying(100) NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(255) NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;



--
-- Column defaults — from setup.sql
--

-- Name: absensi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.absensi ALTER COLUMN id SET DEFAULT nextval('public.absensi_id_seq'::regclass);


--
-- Name: ayat id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ayat ALTER COLUMN id SET DEFAULT nextval('public.ayat_id_seq'::regclass);


--
-- Name: bacaan_sholat id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bacaan_sholat ALTER COLUMN id SET DEFAULT nextval('public.bacaan_sholat_id_seq'::regclass);


--
-- Name: detail_sholat_wajib id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib ALTER COLUMN id SET DEFAULT nextval('public.detail_sholat_wajib_id_seq'::regclass);


--
-- Name: dzikir_setelah_sholat id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dzikir_setelah_sholat ALTER COLUMN id SET DEFAULT nextval('public.dzikir_setelah_sholat_id_seq'::regclass);


--
-- Name: hukum id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hukum ALTER COLUMN id SET DEFAULT nextval('public.hukum_id_seq'::regclass);


--
-- Name: ibadah_harian id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_harian ALTER COLUMN id SET DEFAULT nextval('public.ibadah_harian_id_seq'::regclass);


--
-- Name: ibadah_sunnah id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_sunnah ALTER COLUMN id SET DEFAULT nextval('public.ibadah_sunnah_id_seq'::regclass);


--
-- Name: kategori_sholat_wajib id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kategori_sholat_wajib ALTER COLUMN id SET DEFAULT nextval('public.kategori_sholat_wajib_id_seq'::regclass);


--
-- Name: kategori_sunnah id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kategori_sunnah ALTER COLUMN id SET DEFAULT nextval('public.kategori_sunnah_id_seq'::regclass);


--
-- Name: kegiatan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan ALTER COLUMN id SET DEFAULT nextval('public.kegiatan_id_seq'::regclass);


--
-- Name: kegiatan_user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan_user ALTER COLUMN id SET DEFAULT nextval('public.kegiatan_user_id_seq'::regclass);


--
-- Name: kelas id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kelas ALTER COLUMN id SET DEFAULT nextval('public.kelas_id_seq'::regclass);


--
-- Name: role id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role ALTER COLUMN id SET DEFAULT nextval('public.role_id_seq'::regclass);


--
-- Name: setoran_hafalan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan ALTER COLUMN id SET DEFAULT nextval('public.setoran_hafalan_id_seq'::regclass);


--
-- Name: status_absensi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_absensi ALTER COLUMN id SET DEFAULT nextval('public.status_absensi_id_seq'::regclass);


--
-- Name: status_setoran_hafalan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_setoran_hafalan ALTER COLUMN id SET DEFAULT nextval('public.status_setoran_hafalan_id_seq'::regclass);


--
-- Name: status_sholat_wajib id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_sholat_wajib ALTER COLUMN id SET DEFAULT nextval('public.status_sholat_wajib_id_seq'::regclass);


--
-- Name: surah id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.surah ALTER COLUMN id SET DEFAULT nextval('public.surah_id_seq'::regclass);


--
-- Name: tausiah id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tausiah ALTER COLUMN id SET DEFAULT nextval('public.tausiah_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);



--
-- Seed / reference data — from ramadhan_schema.sql
-- Loaded in dependency-safe order for the lookup tables below.
--

--
-- Data for Name: hukum; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hukum (id, nama) FROM stdin;
1	Wajib / Rukun
2	Sunnah
\.


--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (id, name) FROM stdin;
1	Admin
2	Pembimbing
3	Siswa
\.


--
-- Data for Name: status_absensi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.status_absensi (id, nama) FROM stdin;
1	Hadir
2	Izin
3	Sakit
4	Alfa
\.


--
-- Data for Name: status_setoran_hafalan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.status_setoran_hafalan (id, nama) FROM stdin;
1	Tuntas
2	Belum Tuntas
\.


--
-- Data for Name: status_sholat_wajib; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.status_sholat_wajib (id, nama) FROM stdin;
1	Berjamaah di Masjid
2	Munfarid (Sendiri)
3	Tidak Sholat
\.


--
-- Data for Name: kategori_sholat_wajib; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kategori_sholat_wajib (id, nama) FROM stdin;
1	Subuh
2	Dzuhur
3	Ashar
4	Maghrib
5	Isya
\.


--
-- Data for Name: kategori_sunnah; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kategori_sunnah (id, nama) FROM stdin;
1	Sholat Tarawih
2	Sholat Witir
3	Sholat Dhuha
4	Sholat Tahajud
5	Sedekah Harian
\.


--
-- Data for Name: bacaan_sholat; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bacaan_sholat (id, id_hukum, urutan, nama, gerakan, arabic, translate) FROM stdin;
11	1	1	Takbiratul Ihram	Mengangkat kedua tangan sejajar telinga/bahu	اللهُ أَكْبَرُ	Allah Maha Besar.
12	2	2	Doa Iftitah	Bersedekap (meletakkan tangan kanan di atas tangan kiri di dada/perut)	كَبِيرًا وَالْحَمْدُ لِلَّهِ كَثِيرًا وَسُبْحَانَ اللَّهِ بُكْرَةً وَأَصِيلًا	Allah Maha Besar lagi Sempurna Kebesaran-Nya, segala puji bagi-Nya dengan pujian yang banyak, dan Maha Suci Allah sepanjang pagi dan petang.
13	1	3	Membaca Surah Al-Fatihah	Bersedekap	بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ ۝ الْحَمْدُ لِلَّهِ رَبِّ الْعَالَمِينَ ۝ الرَّحْمَٰنِ الرَّحِيمِ ۝ مَالِكِ يَوْمِ الدِّينِ ۝ إِيَّاكَ نَعْبُدُ وَإِيَّاكَ نَسْتَعِينُ ۝ اهْدِنَا الصِّرَاطَ الْمُسْتَقِيمَ ۝ صِرَاطَ الَّذِينَ أَنْعَمْتَ عَلَيْهِمْ غَيْرِ الْمَغْضُوبِ عَلَيْهِمْ وَلَا الضَّالِّينَ	Dengan nama Allah Yang Maha Pengasih lagi Maha Penyayang. Segala puji bagi Allah, Tuhan seluruh alam. Yang Maha Pengasih lagi Maha Penyayang. Pemilik hari pembalasan. Hanya kepada Engkaulah kami menyembah dan hanya kepada Engkaulah kami memohon pertolongan. Tunjukkanlah kami jalan yang lurus. (Yaitu) jalan orang-orang yang telah Engkau beri nikmat kepadanya; bukan (jalan) mereka yang dimurkai, dan bukan (pula jalan) mereka yang sesat.
14	1	4	Ruku'	Membungkukkan badan, punggung mendatar, dan kedua tangan memegang lutut	سُبْحَانَ رَبِّيَ الْعَظِيمِ وَبِحَمْدِهِ	Maha Suci Tuhanku Yang Maha Agung dan dengan memuji-Nya. (Dibaca 3x)
15	1	5	I'tidal	Bangkit dari ruku' dan berdiri tegak kembali	سَمِعَ اللَّهُ لِمَنْ حَمِدَهُ ، رَبَّنَا لَكَ الْحَمْدُ	Allah mendengar orang yang memuji-Nya. Ya Tuhan kami, bagi-Mu lah segala puji.
16	1	6	Sujud	Meletakkan dahi, hidung, kedua telapak tangan, kedua lutut, dan ujung jari kaki di lantai	سُبْحَانَ رَبِّيَ الْأَعْلَى وَبِحَمْدِهِ	Maha Suci Tuhanku Yang Maha Tinggi dan dengan memuji-Nya. (Dibaca 3x)
17	1	7	Duduk di Antara Dua Sujud	Duduk Iftirasy (duduk di atas telapak kaki kiri dan kaki kanan ditegakkan)	رَبِّ اغْفِرْ لِي وَارْحَمْنِي وَاجْبُرْنِي وَارْفَعْنِي وَارْزُقْنِي وَاهْدِنِي وَعَافِنِي وَاعْفُ عَنِّي	Ya Allah, ampunilah aku, rahmatilah aku, cukupkanlah kekuranganku, tinggikanlah derajatku, berilah aku rezeki, berilah aku petunjuk, sehatkanlah aku, dan maafkanlah aku.
18	1	8	Tasyahud Akhir	Duduk Tawarruk (menyilangkan kaki kiri di bawah kaki kanan)	التَّحِيَّاتُ الْمُبَارَكَاتُ الصَّلَوَاتُ الطَّيِّبَاتُ لِلَّهِ ، السَّلاَمُ عَلَيْكَ أَيُّهَا النَّبِيُّ وَرَحْمَةُ اللَّهِ وَبَرَوَكَاتُهُ ، السَّلاَمُ عَلَيْنَا وَعَلَى عِبَادِ اللَّهِ الصَّالِحِينَ ، أَشْهَدُ أَنْ لاَ إِلَهَ إِلاَّ اللَّهُ وَأَشْهَدُ أَنَّ مُحَمَّدًا رَسُولُ اللَّهِ ، اللَّهُمَّ صَلِّ عَلَى مُحَمَّدٍ وَعَلَى آلِ مُحَمَّدٍ	Segala penghormatan, keberkahan, shalawat dan kebaikan adalah milik Allah. Keselamatan, rahmat Allah, dan berkah-Nya semoga tercurah kepadamu wahai Nabi. Keselamatan semoga tercurah kepada kami dan kepada hamba-hamba Allah yang shalih. Aku bersaksi bahwa tidak ada Tuhan selain Allah, dan aku bersaksi bahwa Muhammad adalah utusan Allah. Ya Allah, berilah shalawat kepada Nabi Muhammad dan keluarga Nabi Muhammad.
19	1	9	Salam	Menolehkan wajah ke kanan lalu ke kiri	السَّلاَمُ عَلَيْكُمْ وَرَحْمَةُ اللَّهِ	Semoga keselamatan dan rahmat Allah tercurah kepadamu.
\.


--
-- Data for Name: dzikir_setelah_sholat; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.dzikir_setelah_sholat (id, nama, arabic, terjemah, sumber) FROM stdin;
11	Istighfar	أَسْتَغْفِرُ اللَّهَ	Aku memohon ampun kepada Allah.	HR. Muslim No. 591
12	Allahumma Antas Salam	اللَّهُمَّ أَنْتَ السَّلَامُ وَمِنْكَ السَّلَامُ تَبَارَكْتَ يَا ذَا الْجَلَالِ وَالْإِكْرَامِ	Ya Allah, Engkau Maha Sejahtera, dari-Mu segala kesejahteraan. Maha Berkah Engkau, wahai Pemilik Keagungan dan Kemuliaan.	HR. Muslim No. 592
13	Tasbih	سُبْحَانَ اللَّهِ	Maha Suci Allah.	HR. Muslim No. 597 (33 kali)
14	Tahmid	الْحَمْدُ لِلَّهِ	Segala puji bagi Allah.	HR. Muslim No. 597 (33 kali)
15	Takbir	اللَّهُ أَكْبَرُ	Allah Maha Besar.	HR. Muslim No. 597 (33 kali)
16	Tahlil Penutup	لَا إِلَهَ إِلَّا اللَّهُ وَحْدَهُ لَا شَرِيكَ لَهُ، لَهُ الْمُلْكُ وَلَهُ الْحَمْدُ، وَهُوَ عَلَى كُلِّ شَيْءٍ قَدِيرٌ	Tiada Tuhan selain Allah semata, tiada sekutu bagi-Nya. Milik-Nya segala kerajaan dan pujian. Dia Maha Kuasa atas segala sesuatu.	HR. Muslim No. 597
17	Ayat Kursi	اللَّهُ لَا إِلَٰهَ إِلَّا هُوَ الْحَيُّ الْقَيُّومُ ۚ لَا تَأْخُذُهُ سِنَةٌ وَلَا نَوْمٌ ۚ لَهُ مَا فِي السَّمَاوَاتِ وَمَا فِي الْأَرْضِ ۗ مَنْ ذَا الَّذِي يَشْفَعُ عِنْدَهُ إِلَّا بِإِذْنِهِ ۚ يَعْلَمُ مَا بَيْنَ أَيْدِيهِمْ وَمَا خَلْفَهُمْ ۖ وَلَا يُحِيطُونَ بِشَيْءٍ مِنْ عِلْمِهِ إِلَّا بِمَا شَاءَ ۚ وَسِعَ كُرْسِيُّهُ السَّمَاوَاتِ وَالْأَرْضَ ۖ وَلَا يَئُودُهُ حِفْظُهُمَا ۚ وَهُوَ الْعَلِيُّ الْعَظِيمُ	Allah, tidak ada Tuhan selain Dia, Yang Maha Hidup lagi terus-menerus mengurus makhluk-Nya... (QS. Al-Baqarah: 255).	QS. Al-Baqarah: 255; HR. An-Nasa'i
18	Surah Al-Ikhlas	قُلْ هُوَ اللَّهُ أَحَدٌ ۝ اللَّهُ الصَّمَدُ ۝ لَمْ يَلِدْ وَلَمْ يُولَدْ ۝ وَلَمْ يَكُنْ لَهُ كُفُوًا أَحَدٌ	Katakanlah: Dialah Allah Yang Maha Esa... (QS. Al-Ikhlas).	QS. Al-Ikhlas; HR. Abu Dawud No. 1523
19	Surah Al-Falaq	قُلْ أَعُوذُ بِرَبِّ الْفَلَقِ ۝ مِنْ شَرِّ مَا خَلَقَ ۝ وَمِنْ شَرِّ غَاسِقٍ إِذَا وَقَبَ ۝ وَمِنْ شَرِّ النَّفَّاثَاتِ فِي الْعُقَدِ ۝ وَمِنْ شَرِّ حَاسِدٍ إِذَا حَسَدَ	Katakanlah: Aku berlindung kepada Tuhan yang menguasai subuh... (QS. Al-Falaq).	QS. Al-Falaq; HR. Abu Dawud No. 1523
20	Surah An-Nas	قُلْ أَعُوذُ بِرَبِّ النَّاسِ ۝ مَلِكِ النَّاسِ ۝ إِلَٰهِ النَّاسِ ۝ مِنْ شَرِّ الْوَسْوَاسِ الْخَنَّاسِ ۝ الَّذِي يُوَسْوِسُ فِي صُدُورِ النَّاسِ ۝ مِنَ الْجِنَّةِ وَالنَّاسِ	Katakanlah: Aku berlindung kepada Tuhan manusia... (QS. An-Nas).	QS. An-Nas; HR. Abu Dawud No. 1523
\.


--
-- NOTE: The two blocks below (ayat, detail_sholat_wajib) reference tables
-- (surah, ibadah_harian) whose data is NOT included in either source file.
-- They are included here for completeness, but COPY will fail against a
-- fresh database until public.surah and public.ibadah_harian are populated
-- with rows matching the id_surah / id_ibadah_harian values referenced below
-- (or you load them before this section / drop the FK checks temporarily).
--

--
-- Data for Name: ayat; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ayat (id, id_surah, nomor, arab, terjemah) FROM stdin;
2	1	1	عَمَّ يَتَسَاۤءَلُوْنَۚ	Tentang apakah mereka saling bertanya?
3	1	2	عَنِ النَّبَاِ الْعَظِيْمِۙ	Tentang berita yang besar (hari Kebangkitan)
4	1	3	الَّذِيْ هُمْ فِيْهِ مُخْتَلِفُوْنَۗ	yang dalam hal itu mereka berselisih.
5	1	4	كَلَّا سَيَعْلَمُوْنَۙ	Sekali-kali tidak! Kelak mereka akan mengetahui.
6	1	5	ثُمَّ كَلَّا سَيَعْلَمُوْنَ	Sekali lagi, tidak! Kelak mereka akan mengetahui.
7	1	6	اَلَمْ نَجْعَلِ الْاَرْضَ مِهٰدًاۙ	Bukankah Kami telah menjadikan bumi sebagai hamparan
8	1	7	وَّالْجِبَالَ اَوْتَادًاۖ	dan gunung-gunung sebagai pasak?
9	1	8	وَّخَلَقْنٰكُمْ اَزْوَاجًاۙ	Kami menciptakan kamu berpasang-pasangan.
10	1	9	وَّجَعَلْنَا نَوْمَكُمْ سُبَاتًاۙ	Kami menjadikan tidurmu untuk beristirahat.
11	1	10	وَّجَعَلْنَا الَّيْلَ لِبَاسًاۙ	Kami menjadikan malam sebagai pakaian.
12	1	11	وَّجَعَلْنَا النَّهَارَ مَعَاشًاۚ	Kami menjadikan siang untuk mencari penghidupan.
13	1	12	وَبَنَيْنَا فَوْقَكُمْ سَبْعًا شِدَادًاۙ	Kami membangun tujuh (langit) yang kukuh di atasmu.
14	1	13	وَّجَعَلْنَا سِرَاجًا وَّهَّاجًاۖ	Kami menjadikan pelita yang terang-benderang (matahari).
15	1	14	وَّاَنْزَلْنَا مِنَ الْمُعْصِرٰتِ مَاۤءً ثَجَّاجًاۙ	Kami menurunkan dari awan air hujan yang tercurah dengan deras
16	1	15	لِّنُخْرِجَ بِهٖ حَبًّا وَّنَبَاتًاۙ	agar Kami menumbuhkan dengannya biji-bijian, tanam-tanaman,
17	1	16	وَّجَنّٰتٍ اَلْفَافًاۗ	dan kebun-kebun yang rindang.
18	1	17	اِنَّ يَوْمَ الْفَصْلِ كَانَ مِيْقَاتًاۙ	Sesungguhnya hari Keputusan itu adalah waktu yang telah ditetapkan,
19	1	18	يَّوْمَ يُنْفَخُ فِى الصُّوْرِ فَتَأْتُوْنَ اَفْوَاجًاۙ	(yaitu) hari (ketika) sangkakala ditiup, lalu kamu datang berbondong-bondong.
20	1	19	وَّفُتِحَتِ السَّمَاۤءُ فَكَانَتْ اَبْوَابًاۙ	Langit pun dibuka. Maka, terdapatlah beberapa pintu.
21	1	20	وَّسُيِّرَتِ الْجِبَالُ فَكَانَتْ سَرَابًاۗ	Gunung-gunung pun dijalankan. Maka, ia menjadi (seperti) fatamorgana.
22	1	21	اِنَّ جَهَنَّمَ كَانَتْ مِرْصَادًاۙ	Sesungguhnya (neraka) Jahanam itu (merupakan) tempat mengintai (bagi penjaga neraka)
23	1	22	لِّلطّٰغِيْنَ مَاٰبًاۙ	(dan) menjadi tempat kembali bagi orang-orang yang melampaui batas.
24	1	23	لّٰبِثِيْنَ فِيْهَآ اَحْقَابًاۚ	Mereka tinggal di sana dalam masa yang lama.
25	1	24	لَا يَذُوْقُوْنَ فِيْهَا بَرْدًا وَّلَا شَرَابًاۙ	Mereka tidak merasakan kesejukan di dalamnya dan tidak (pula mendapat) minuman,
26	1	25	اِلَّا حَمِيْمًا وَّغَسَّاقًاۙ	selain air yang mendidih dan nanah,
27	1	26	جَزَاۤءً وِّفَاقًاۗ	sebagai pembalasan yang setimpal.
28	1	27	اِنَّهُمْ كَانُوْا لَا يَرْجُوْنَ حِسَابًاۙ	Sesungguhnya mereka tidak pernah mengharapkan perhitungan.
29	1	28	وَّكَذَّبُوْا بِاٰيٰتِنَا كِذَّابًاۗ	Mereka benar-benar mendustakan ayat-ayat Kami.
30	1	29	وَكُلَّ شَيْءٍ اَحْصَيْنٰهُ كِتٰبًاۙ	Segala sesuatu telah Kami catat dalam kitab (catatan amal manusia).
31	1	30	فَذُوْقُوْا فَلَنْ نَّزِيْدَكُمْ اِلَّا عَذَابًا ࣖ	Oleh karena itu, rasakanlah! Tidak akan Kami tambahkan kepadamu, kecuali azab.
32	1	31	اِنَّ لِلْمُتَّقِيْنَ مَفَازًاۙ	Sesungguhnya bagi orang-orang yang bertakwa (ada) kemenangan (surga),
33	1	32	حَدَاۤىِٕقَ وَاَعْنَابًاۙ	(yaitu) kebun-kebun, buah anggur,
34	1	33	وَّكَوَاعِبَ اَتْرَابًاۙ	gadis-gadis molek yang sebaya,
35	1	34	وَّكَأْسًا دِهَاقًاۗ	dan gelas-gelas yang penuh (berisi minuman).
36	1	35	لَا يَسْمَعُوْنَ فِيْهَا لَغْوًا وَّلَا كِذّٰبًا	Di sana mereka tidak mendengar percakapan yang sia-sia dan tidak pula (perkataan) dusta.
37	1	36	جَزَاۤءً مِّنْ رَّبِّكَ عَطَاۤءً حِسَابًاۙ	(Hal itu) sebagai balasan (dan) pemberian yang banyak dari Tuhanmu,
38	1	37	رَّبِّ السَّمٰوٰتِ وَالْاَرْضِ وَمَا بَيْنَهُمَا الرَّحْمٰنِ لَا يَمْلِكُوْنَ مِنْهُ خِطَابًاۚ	(yaitu) Tuhan (pemelihara) langit, bumi, dan apa yang ada di antara keduanya, Yang Maha Pengasih. Mereka tidak memiliki (hak) berbicara dengan-Nya.
39	1	38	يَوْمَ يَقُوْمُ الرُّوْحُ وَالْمَلٰۤىِٕكَةُ صَفًّاۙ لَّا يَتَكَلَّمُوْنَ اِلَّا مَنْ اَذِنَ لَهُ الرَّحْمٰنُ وَقَالَ صَوَابًا	Pada hari ketika Rūḥ dan malaikat berdiri bersaf-saf. Mereka tidak berbicara, kecuali yang diizinkan oleh Tuhan Yang Maha Pengasih dan dia mengatakan yang benar.
40	1	39	ذٰلِكَ الْيَوْمُ الْحَقُّۚ فَمَنْ شَاۤءَ اتَّخَذَ اِلٰى رَبِّهٖ مَاٰبًا	Itulah hari yang hak (pasti terjadi). Siapa yang menghendaki (keselamatan) niscaya menempuh jalan kembali kepada Tuhannya (dengan beramal saleh).
41	1	40	اِنَّآ اَنْذَرْنٰكُمْ عَذَابًا قَرِيْبًا ەۙ يَّوْمَ يَنْظُرُ الْمَرْءُ مَا قَدَّمَتْ يَدَاهُ وَيَقُوْلُ الْكٰفِرُ يٰلَيْتَنِيْ كُنْتُ تُرٰبًا ࣖ	Sesungguhnya Kami telah memperingatkan kamu akan azab yang dekat pada hari (ketika) manusia melihat apa yang telah diperbuat oleh kedua tangannya dan orang kafir berkata, “Oh, seandainya saja aku menjadi tanah.”
42	2	1	وَالنّٰزِعٰتِ غَرْقًاۙ	Demi (malaikat) yang mencabut (nyawa orang kafir) dengan keras,
43	2	2	وَّالنّٰشِطٰتِ نَشْطًاۙ	demi (malaikat) yang mencabut (nyawa orang mukmin) dengan lemah lembut,
44	2	3	وَّالسّٰبِحٰتِ سَبْحًاۙ	demi (malaikat) yang cepat (menunaikan tugasnya) dengan mudah,
45	2	4	فَالسّٰبِقٰتِ سَبْقًاۙ	(malaikat) yang bergegas (melaksanakan perintah Allah) dengan cepat,
46	2	5	فَالْمُدَبِّرٰتِ اَمْرًاۘ	dan (malaikat) yang mengatur urusan (dunia),
47	2	6	يَوْمَ تَرْجُفُ الرَّاجِفَةُۙ	(kamu benar-benar akan dibangkitkan) pada hari ketika tiupan pertama mengguncang (alam semesta).
48	2	7	تَتْبَعُهَا الرَّادِفَةُ ۗ	(Tiupan pertama) itu diiringi oleh tiupan kedua.
49	2	8	قُلُوْبٌ يَّوْمَىِٕذٍ وَّاجِفَةٌۙ	Hati manusia pada hari itu merasa sangat takut;
50	2	9	اَبْصَارُهَا خَاشِعَةٌ	pandangannya tertunduk.
51	2	10	يَقُوْلُوْنَ ءَاِنَّا لَمَرْدُوْدُوْنَ فِى الْحَافِرَةِۗ	Mereka (di dunia) berkata, “Apakah kita benar-benar akan dikembalikan pada kehidupan yang semula?
52	2	11	ءَاِذَا كُنَّا عِظَامًا نَّخِرَةً ۗ	Apabila kita telah menjadi tulang-belulang yang hancur, apakah kita (akan dibangkitkan juga)?”
53	2	12	قَالُوْا تِلْكَ اِذًا كَرَّةٌ خَاسِرَةٌ ۘ	Mereka berkata, “Kalau demikian, itu suatu pengembalian yang merugikan.”
54	2	13	فَاِنَّمَا هِيَ زَجْرَةٌ وَّاحِدَةٌۙ	(Jangan dianggap sulit,) pengembalian itu (dilakukan) hanyalah dengan sekali tiupan.
55	2	14	فَاِذَا هُمْ بِالسَّاهِرَةِۗ	Seketika itu, mereka hidup kembali di bumi (yang baru).
56	2	15	هَلْ اَتٰىكَ حَدِيْثُ مُوْسٰىۘ	Sudah sampaikah kepadamu (Nabi Muhammad) kisah Musa?
57	2	16	اِذْ نَادٰىهُ رَبُّهٗ بِالْوَادِ الْمُقَدَّسِ طُوًىۚ	(Ingatlah) ketika Tuhannya menyeru dia (Musa) di lembah suci, yaitu Lembah Tuwa,
58	2	17	اِذْهَبْ اِلٰى فِرْعَوْنَ اِنَّهٗ طَغٰىۖ	“Pergilah engkau kepada Fir‘aun! Sesungguhnya dia telah melampaui batas.
59	2	18	فَقُلْ هَلْ لَّكَ اِلٰٓى اَنْ تَزَكّٰىۙ	Lalu, katakanlah (kepada Fir‘aun), ‘Adakah keinginanmu untuk menyucikan diri (dari kesesatan)
60	2	19	وَاَهْدِيَكَ اِلٰى رَبِّكَ فَتَخْشٰىۚ	dan aku akan menunjukimu ke (jalan) Tuhanmu agar engkau takut (kepada-Nya)?’”
61	2	20	فَاَرٰىهُ الْاٰيَةَ الْكُبْرٰىۖ	Lalu, dia (Musa) memperlihatkan mukjizat yang besar kepadanya.
62	2	21	فَكَذَّبَ وَعَصٰىۖ	Akan tetapi, dia (Fir‘aun) mendustakan (kerasulan) dan mendurhakai (Allah).
63	2	22	ثُمَّ اَدْبَرَ يَسْعٰىۖ	Kemudian, dia berpaling seraya berusaha (menantang Musa).
64	2	23	فَحَشَرَ فَنَادٰىۖ	Maka, dia mengumpulkan (pembesar-pembesarnya), lalu berseru (memanggil kaumnya).
65	2	24	فَقَالَ اَنَا۠ رَبُّكُمُ الْاَعْلٰىۖ	Dia berkata, “Akulah Tuhanmu yang paling tinggi.”
66	2	25	فَاَخَذَهُ اللّٰهُ نَكَالَ الْاٰخِرَةِ وَالْاُوْلٰىۗ	Maka, Allah menghukumnya dengan azab di akhirat dan (siksaan) di dunia.
67	2	26	اِنَّ فِيْ ذٰلِكَ لَعِبْرَةً لِّمَنْ يَّخْشٰى ۗ ࣖ	Sesungguhnya pada yang demikian itu benar-benar terdapat pelajaran bagi orang yang takut (kepada Allah).
68	2	27	ءَاَنْتُمْ اَشَدُّ خَلْقًا اَمِ السَّمَاۤءُ ۚ بَنٰىهَاۗ	Apakah penciptaan kamu yang lebih hebat ataukah langit yang telah dibangun-Nya?
69	2	28	رَفَعَ سَمْكَهَا فَسَوّٰىهَاۙ	Dia telah meninggikan bangunannya, lalu menyempurnakannya.
70	2	29	وَاَغْطَشَ لَيْلَهَا وَاَخْرَجَ ضُحٰىهَاۖ	Dia menjadikan malamnya (gelap gulita) dan menjadikan siangnya (terang benderang).
71	2	30	وَالْاَرْضَ بَعْدَ ذٰلِكَ دَحٰىهَاۗ	Setelah itu, bumi Dia hamparkan (untuk dihuni).
72	2	31	اَخْرَجَ مِنْهَا مَاۤءَهَا وَمَرْعٰىهَاۖ	Darinya (bumi) Dia mengeluarkan air dan (menyediakan) tempat penggembalaan.
73	2	32	وَالْجِبَالَ اَرْسٰىهَاۙ	Gunung-gunung Dia pancangkan dengan kukuh.
74	2	33	مَتَاعًا لَّكُمْ وَلِاَنْعَامِكُمْۗ	(Semua itu disediakan) untuk kesenanganmu dan hewan ternakmu.
75	2	34	فَاِذَا جَاۤءَتِ الطَّاۤمَّةُ الْكُبْرٰىۖ	Maka, apabila malapetaka terbesar (hari Kiamat) telah datang,
76	2	35	يَوْمَ يَتَذَكَّرُ الْاِنْسَانُ مَا سَعٰىۙ	pada hari (itu) manusia teringat apa yang telah dikerjakannya
77	2	36	وَبُرِّزَتِ الْجَحِيْمُ لِمَنْ يَّرٰى	dan (neraka) Jahim diperlihatkan dengan jelas kepada orang yang melihat(-nya).
78	2	37	فَاَمَّا مَنْ طَغٰىۖ	Adapun orang yang melampaui batas
79	2	38	وَاٰثَرَ الْحَيٰوةَ الدُّنْيَاۙ	dan lebih mengutamakan kehidupan dunia,
80	2	39	فَاِنَّ الْجَحِيْمَ هِيَ الْمَأْوٰىۗ	sesungguhnya (neraka) Jahimlah tempat tinggal(-nya).
81	2	40	وَاَمَّا مَنْ خَافَ مَقَامَ رَبِّهٖ وَنَهَى النَّفْسَ عَنِ الْهَوٰىۙ	Adapun orang-orang yang takut pada kebesaran Tuhannya dan menahan diri dari (keinginan) hawa nafsunya,
82	2	41	فَاِنَّ الْجَنَّةَ هِيَ الْمَأْوٰىۗ	sesungguhnya surgalah tempat tinggal(-nya).
83	2	42	يَسْـَٔلُوْنَكَ عَنِ السَّاعَةِ اَيَّانَ مُرْسٰىهَاۗ	Mereka (orang-orang kafir) bertanya kepadamu (Nabi Muhammad) tentang hari Kiamat, “Kapankah terjadinya?”
84	2	43	فِيْمَ اَنْتَ مِنْ ذِكْرٰىهَاۗ	Untuk apa engkau perlu menyebutkan (waktu)-nya?
85	2	44	اِلٰى رَبِّكَ مُنْتَهٰىهَاۗ	Kepada Tuhanmulah (dikembalikan) kesudahan (ketentuan waktu)-nya.
86	2	45	اِنَّمَآ اَنْتَ مُنْذِرُ مَنْ يَّخْشٰىهَاۗ	Engkau (Nabi Muhammad) hanyalah pemberi peringatan kepada siapa yang takut padanya (hari Kiamat).
87	2	46	كَاَنَّهُمْ يَوْمَ يَرَوْنَهَا لَمْ يَلْبَثُوْٓا اِلَّا عَشِيَّةً اَوْ ضُحٰىهَا ࣖ	Pada hari ketika melihatnya (hari Kiamat itu), mereka merasa seakan-akan hanya (sebentar) tinggal (di dunia) pada waktu petang atau pagi.
88	3	1	عَبَسَ وَتَوَلّٰىٓۙ	Dia (Nabi Muhammad) berwajah masam dan berpaling
89	3	2	اَنْ جَاۤءَهُ الْاَعْمٰىۗ	karena seorang tunanetra (Abdullah bin Ummi Maktum) telah datang kepadanya.
90	3	3	وَمَا يُدْرِيْكَ لَعَلَّهٗ يَزَّكّٰىٓۙ	Tahukah engkau (Nabi Muhammad) boleh jadi dia ingin menyucikan dirinya (dari dosa)
91	3	4	اَوْ يَذَّكَّرُ فَتَنْفَعَهُ الذِّكْرٰىۗ	atau dia (ingin) mendapatkan pengajaran sehingga pengajaran itu bermanfaat baginya?
92	3	5	اَمَّا مَنِ اسْتَغْنٰىۙ	Adapun orang yang merasa dirinya serba cukup (para pembesar Quraisy),
93	3	6	فَاَنْتَ لَهٗ تَصَدّٰىۗ	engkau (Nabi Muhammad) memberi perhatian kepadanya.
94	3	7	وَمَا عَلَيْكَ اَلَّا يَزَّكّٰىۗ	Padahal, tidak ada (cela) atasmu kalau dia tidak menyucikan diri (beriman).
95	3	8	وَاَمَّا مَنْ جَاۤءَكَ يَسْعٰىۙ	Adapun orang yang datang kepadamu dengan bersegera (untuk mendapatkan pengajaran),
96	3	9	وَهُوَ يَخْشٰىۙ	sedangkan dia takut (kepada Allah),
97	3	10	فَاَنْتَ عَنْهُ تَلَهّٰىۚ	malah engkau (Nabi Muhammad) abaikan.
98	3	11	كَلَّآ اِنَّهَا تَذْكِرَةٌ ۚ	Sekali-kali jangan (begitu)! Sesungguhnya (ajaran Allah) itu merupakan peringatan.
99	3	12	فَمَنْ شَاۤءَ ذَكَرَهٗ ۘ	Siapa yang menghendaki tentulah akan memperhatikannya
100	3	13	فِيْ صُحُفٍ مُّكَرَّمَةٍۙ	di dalam suhuf yang dimuliakan (di sisi Allah),
101	3	14	مَّرْفُوْعَةٍ مُّطَهَّرَةٍ ۢ ۙ	yang ditinggikan (kedudukannya) lagi disucikan
102	3	15	بِاَيْدِيْ سَفَرَةٍۙ	di tangan para utusan (malaikat)
103	3	16	كِرَامٍۢ بَرَرَةٍۗ	yang mulia lagi berbudi.
104	3	17	قُتِلَ الْاِنْسَانُ مَآ اَكْفَرَهٗۗ	Celakalah manusia! Alangkah kufur dia!
105	3	18	مِنْ اَيِّ شَيْءٍ خَلَقَهٗۗ	Dari apakah Dia menciptakannya?
106	3	19	مِنْ نُّطْفَةٍۗ خَلَقَهٗ فَقَدَّرَهٗۗ	Dia menciptakannya dari setetes mani, lalu menentukan (takdir)-nya.
107	3	20	ثُمَّ السَّبِيْلَ يَسَّرَهٗۙ	Kemudian, jalannya Dia mudahkan.
108	3	21	ثُمَّ اَمَاتَهٗ فَاَقْبَرَهٗۙ	Kemudian, Dia mematikannya lalu menguburkannya.
109	3	22	ثُمَّ اِذَا شَاۤءَ اَنْشَرَهٗۗ	Kemudian, jika menghendaki, Dia membangkitkannya kembali.
110	3	23	كَلَّا لَمَّا يَقْضِ مَآ اَمَرَهٗۗ	Sekali-kali jangan (begitu)! Dia (manusia) itu belum melaksanakan apa yang Dia (Allah) perintahkan kepadanya.
111	3	24	فَلْيَنْظُرِ الْاِنْسَانُ اِلٰى طَعَامِهٖٓ ۙ	Maka, hendaklah manusia itu memperhatikan makanannya.
112	3	25	اَنَّا صَبَبْنَا الْمَاۤءَ صَبًّاۙ	Sesungguhnya Kami telah mencurahkan air (dari langit) dengan berlimpah.
113	3	26	ثُمَّ شَقَقْنَا الْاَرْضَ شَقًّاۙ	Kemudian, Kami belah bumi dengan sebaik-baiknya.
114	3	27	فَاَنْۢبَتْنَا فِيْهَا حَبًّاۙ	Lalu, Kami tumbuhkan padanya biji-bijian,
115	3	28	وَّعِنَبًا وَّقَضْبًاۙ	anggur, sayur-sayuran,
116	3	29	وَّزَيْتُوْنًا وَّنَخْلًاۙ	zaitun, pohon kurma,
117	3	30	وَّحَدَاۤىِٕقَ غُلْبًا	kebun-kebun (yang) rindang,
118	3	31	وَفَاكِهَةً وَّاَبًّا	buah-buahan, dan rerumputan.
119	3	32	مَتَاعًا لَّكُمْ وَلِاَنْعَامِكُمْۗ	(Semua itu disediakan) untuk kesenanganmu dan hewan-hewan ternakmu.
120	3	33	فَاِذَا جَاۤءَتِ الصَّاۤخَّةُ ۖ	Maka, apabila datang suara yang memekakkan (dari tiupan sangkakala),
121	3	34	يَوْمَ يَفِرُّ الْمَرْءُ مِنْ اَخِيْهِۙ	pada hari itu manusia lari dari saudaranya,
122	3	35	وَاُمِّهٖ وَاَبِيْهِۙ	(dari) ibu dan bapaknya,
123	3	36	وَصَاحِبَتِهٖ وَبَنِيْهِۗ	serta (dari) istri dan anak-anaknya.
124	3	37	لِكُلِّ امْرِئٍ مِّنْهُمْ يَوْمَىِٕذٍ شَأْنٌ يُّغْنِيْهِۗ	Setiap orang dari mereka pada hari itu mempunyai urusan yang menyibukkannya.
125	3	38	وُجُوْهٌ يَّوْمَىِٕذٍ مُّسْفِرَةٌۙ	Pada hari itu ada wajah-wajah yang berseri-seri,
126	3	39	ضَاحِكَةٌ مُّسْتَبْشِرَةٌ ۚ	tertawa lagi gembira ria.
127	3	40	وَوُجُوْهٌ يَّوْمَىِٕذٍ عَلَيْهَا غَبَرَةٌۙ	Pada hari itu ada (pula) wajah-wajah yang tertutup debu (suram)
128	3	41	تَرْهَقُهَا قَتَرَةٌ ۗ	dan tertutup oleh kegelapan (ditimpa kehinaan dan kesusahan).
129	3	42	اُولٰۤىِٕكَ هُمُ الْكَفَرَةُ الْفَجَرَةُ ࣖ	Mereka itulah orang-orang kafir lagi para pendurhaka.
130	4	1	اِذَا الشَّمْسُ كُوِّرَتْۖ	Apabila matahari digulung,
131	4	2	وَاِذَا النُّجُوْمُ انْكَدَرَتْۖ	apabila bintang-bintang berjatuhan,
132	4	3	وَاِذَا الْجِبَالُ سُيِّرَتْۖ	apabila gunung-gunung dihancurkan,
133	4	4	وَاِذَا الْعِشَارُ عُطِّلَتْۖ	apabila unta-unta yang bunting ditinggalkan (tidak terurus),
134	4	5	وَاِذَا الْوُحُوْشُ حُشِرَتْۖ	apabila binatang-binatang liar dikumpulkan,
135	4	6	وَاِذَا الْبِحَارُ سُجِّرَتْۖ	apabila lautan dipanaskan,
136	4	7	وَاِذَا النُّفُوْسُ زُوِّجَتْۖ	apabila roh-roh dipertemukan (dengan tubuh),
137	4	8	وَاِذَا الْمَوْءٗدَةُ سُىِٕلَتْۖ	apabila bayi-bayi perempuan yang dikubur hidup-hidup ditanya,
138	4	9	بِاَيِّ ذَنْۢبٍ قُتِلَتْۚ	“Karena dosa apa dia dibunuh,”
139	4	10	وَاِذَا الصُّحُفُ نُشِرَتْۖ	apabila lembaran-lembaran (catatan amal) telah dibuka lebar-lebar,
140	4	11	وَاِذَا السَّمَاۤءُ كُشِطَتْۖ	apabila langit dilenyapkan,
141	4	12	وَاِذَا الْجَحِيْمُ سُعِّرَتْۖ	apabila (neraka) Jahim dinyalakan,
142	4	13	وَاِذَا الْجَنَّةُ اُزْلِفَتْۖ	dan apabila surga didekatkan,
143	4	14	عَلِمَتْ نَفْسٌ مَّآ اَحْضَرَتْۗ	setiap jiwa akan mengetahui apa yang telah dikerjakannya.
144	4	15	فَلَآ اُقْسِمُ بِالْخُنَّسِۙ	Aku bersumpah demi bintang-bintang
145	4	16	الْجَوَارِ الْكُنَّسِۙ	yang beredar lagi terbenam,
146	4	17	وَالَّيْلِ اِذَا عَسْعَسَۙ	demi malam apabila telah larut,
147	4	18	وَالصُّبْحِ اِذَا تَنَفَّسَۙ	demi subuh apabila (fajar) telah menyingsing,
148	4	19	اِنَّهٗ لَقَوْلُ رَسُوْلٍ كَرِيْمٍۙ	sesungguhnya (Al-Qur’an) itu benar-benar firman (Allah yang dibawa oleh) utusan yang mulia (Jibril)
149	4	20	ذِيْ قُوَّةٍ عِنْدَ ذِى الْعَرْشِ مَكِيْنٍۙ	yang memiliki kekuatan dan kedudukan tinggi di sisi (Allah) yang memiliki ʻArasy,
150	4	21	مُّطَاعٍ ثَمَّ اَمِيْنٍۗ	yang di sana (Jibril) ditaati lagi dipercaya.
151	4	22	وَمَا صَاحِبُكُمْ بِمَجْنُوْنٍۚ	Temanmu (Nabi Muhammad) itu bukanlah orang gila.
152	4	23	وَلَقَدْ رَاٰهُ بِالْاُفُقِ الْمُبِيْنِۚ	Sungguh, dia (Nabi Muhammad) benar-benar telah melihatnya (Jibril) di ufuk yang terang.
153	4	24	وَمَا هُوَ عَلَى الْغَيْبِ بِضَنِيْنٍۚ	Dia (Nabi Muhammad) bukanlah seorang yang kikir (enggan) untuk menerangkan yang gaib.
154	4	25	وَمَا هُوَ بِقَوْلِ شَيْطٰنٍ رَّجِيْمٍۚ	(Al-Qur’an) itu bukanlah perkataan setan yang terkutuk.
155	4	26	فَاَيْنَ تَذْهَبُوْنَۗ	Maka, ke manakah kamu akan pergi?
156	4	27	اِنْ هُوَ اِلَّا ذِكْرٌ لِّلْعٰلَمِيْنَۙ	(Al-Qur’an) itu tidak lain, kecuali peringatan bagi semesta alam,
157	4	28	لِمَنْ شَاۤءَ مِنْكُمْ اَنْ يَّسْتَقِيْمَۗ	(yaitu) bagi siapa di antaramu yang hendak menempuh jalan yang lurus.
158	4	29	وَمَا تَشَاۤءُوْنَ اِلَّآ اَنْ يَّشَاۤءَ اللّٰهُ رَبُّ الْعٰلَمِيْنَ ࣖ	Kamu tidak dapat berkehendak, kecuali apabila dikehendaki Allah, Tuhan semesta alam.
159	5	1	اِذَا السَّمَاۤءُ انْفَطَرَتْۙ	Apabila langit terbelah,
160	5	2	وَاِذَا الْكَوَاكِبُ انْتَثَرَتْۙ	apabila bintang-bintang jatuh berserakan,
161	5	3	وَاِذَا الْبِحَارُ فُجِّرَتْۙ	apabila lautan diluapkan,
162	5	4	وَاِذَا الْقُبُوْرُ بُعْثِرَتْۙ	dan apabila kuburan-kuburan dibongkar,
163	5	5	عَلِمَتْ نَفْسٌ مَّا قَدَّمَتْ وَاَخَّرَتْۗ	setiap jiwa akan mengetahui apa yang telah dikerjakan dan yang dilalaikan(-nya).
164	5	6	يٰٓاَيُّهَا الْاِنْسَانُ مَا غَرَّكَ بِرَبِّكَ الْكَرِيْمِۙ	Wahai manusia, apakah yang telah memperdayakanmu (berbuat durhaka) terhadap Tuhanmu Yang Maha Mulia,
165	5	7	الَّذِيْ خَلَقَكَ فَسَوّٰىكَ فَعَدَلَكَۙ	yang telah menciptakanmu lalu menyempurnakan kejadianmu dan menjadikan (susunan tubuh)-mu seimbang?
166	5	8	فِيْٓ اَيِّ صُوْرَةٍ مَّا شَاۤءَ رَكَّبَكَۗ	Dalam bentuk apa saja yang dikehendaki, Dia menyusun (tubuh)-mu.
167	5	9	كَلَّا بَلْ تُكَذِّبُوْنَ بِالدِّيْنِۙ	Jangan sekali-kali begitu! Bahkan, kamu mendustakan hari Pembalasan.
168	5	10	وَاِنَّ عَلَيْكُمْ لَحٰفِظِيْنَۙ	Sesungguhnya bagi kamu ada (malaikat-malaikat) pengawas
169	5	11	كِرَامًا كٰتِبِيْنَۙ	yang mulia (di sisi Allah) dan mencatat (amal perbuatanmu).
170	5	12	يَعْلَمُوْنَ مَا تَفْعَلُوْنَ	Mereka mengetahui apa yang kamu kerjakan.
171	5	13	اِنَّ الْاَبْرَارَ لَفِيْ نَعِيْمٍۙ	Sesungguhnya orang-orang yang berbakti benar-benar berada dalam (surga yang penuh) kenikmatan.
172	5	14	وَّاِنَّ الْفُجَّارَ لَفِيْ جَحِيْمٍ	Sesungguhnya orang-orang yang durhaka benar-benar berada dalam (neraka) Jahim.
173	5	15	يَصْلَوْنَهَا يَوْمَ الدِّيْنِ	Mereka memasukinya pada hari Pembalasan.
174	5	16	وَمَا هُمْ عَنْهَا بِغَاۤىِٕبِيْنَۗ	Mereka tidak mungkin keluar dari (neraka) itu.
175	5	17	وَمَآ اَدْرٰىكَ مَا يَوْمُ الدِّيْنِۙ	Tahukah engkau apakah hari Pembalasan itu?
176	5	18	ثُمَّ مَآ اَدْرٰىكَ مَا يَوْمُ الدِّيْنِۗ	Kemudian, tahukah engkau apakah hari Pembalasan itu?
177	5	19	يَوْمَ لَا تَمْلِكُ نَفْسٌ لِّنَفْسٍ شَيْـًٔا ۗوَالْاَمْرُ يَوْمَىِٕذٍ لِّلّٰهِ ࣖ	(Itulah) hari (ketika) seseorang tidak berdaya (menolong) orang lain sedikit pun. Segala urusan pada hari itu adalah milik Allah.
178	6	1	وَيْلٌ لِّلْمُطَفِّفِيْنَۙ	Celakalah orang-orang yang curang (dalam menakar dan menimbang)!
179	6	2	الَّذِيْنَ اِذَا اكْتَالُوْا عَلَى النَّاسِ يَسْتَوْفُوْنَۖ	(Mereka adalah) orang-orang yang apabila menerima takaran dari orang lain, mereka minta dipenuhi.
180	6	3	وَاِذَا كَالُوْهُمْ اَوْ وَّزَنُوْهُمْ يُخْسِرُوْنَۗ	(Sebaliknya,) apabila mereka menakar atau menimbang untuk orang lain, mereka kurangi.
181	6	4	اَلَا يَظُنُّ اُولٰۤىِٕكَ اَنَّهُمْ مَّبْعُوْثُوْنَۙ	Tidakkah mereka mengira (bahwa) sesungguhnya mereka akan dibangkitkan
182	6	5	لِيَوْمٍ عَظِيْمٍۙ	pada suatu hari yang besar (Kiamat),
183	6	6	يَّوْمَ يَقُوْمُ النَّاسُ لِرَبِّ الْعٰلَمِيْنَۗ	(yaitu) hari (ketika) manusia bangkit menghadap Tuhan seluruh alam?
184	6	7	كَلَّآ اِنَّ كِتٰبَ الْفُجَّارِ لَفِيْ سِجِّيْنٍۗ	Jangan sekali-kali begitu! Sesungguhnya catatan orang yang durhaka benar-benar (tersimpan) dalam Sijjīn.
185	6	8	وَمَآ اَدْرٰىكَ مَا سِجِّيْنٌۗ	Tahukah engkau apakah Sijjīn itu?
186	6	9	كِتٰبٌ مَّرْقُوْمٌۗ	(Ia adalah) kitab yang berisi catatan (amal).
187	6	10	وَيْلٌ يَّوْمَىِٕذٍ لِّلْمُكَذِّبِيْنَۙ	Celakalah pada hari itu bagi para pendusta,
188	6	11	الَّذِيْنَ يُكَذِّبُوْنَ بِيَوْمِ الدِّيْنِۗ	yaitu orang-orang yang mendustakan hari Pembalasan.
189	6	12	وَمَا يُكَذِّبُ بِهٖٓ اِلَّا كُلُّ مُعْتَدٍ اَثِيْمٍۙ	Tidak ada yang mendustakannya, kecuali setiap orang yang melampaui batas lagi sangat berdosa.
190	6	13	اِذَا تُتْلٰى عَلَيْهِ اٰيٰتُنَا قَالَ اَسَاطِيْرُ الْاَوَّلِيْنَۗ	Apabila dibacakan kepadanya ayat-ayat Kami, dia berkata, “(Itu adalah) dongeng orang-orang dahulu.”
191	6	14	كَلَّا بَلْ ۜرَانَ عَلٰى قُلُوْبِهِمْ مَّا كَانُوْا يَكْسِبُوْنَ	Sekali-kali tidak! Bahkan, apa yang selalu mereka kerjakan itu telah menutupi hati mereka.
192	6	15	كَلَّآ اِنَّهُمْ عَنْ رَّبِّهِمْ يَوْمَىِٕذٍ لَّمَحْجُوْبُوْنَۗ	Sekali-kali tidak! Sesungguhnya mereka pada hari itu benar-benar terhalang dari (rahmat) Tuhannya.
193	6	16	ثُمَّ اِنَّهُمْ لَصَالُوا الْجَحِيْمِۗ	Sesungguhnya mereka kemudian benar-benar masuk (neraka) Jahim.
194	6	17	ثُمَّ يُقَالُ هٰذَا الَّذِيْ كُنْتُمْ بِهٖ تُكَذِّبُوْنَۗ	Lalu dikatakan (kepada mereka), “Inilah (azab) yang selalu kamu dustakan.”
195	6	18	كَلَّآ اِنَّ كِتٰبَ الْاَبْرَارِ لَفِيْ عِلِّيِّيْنَۗ	Sekali-kali tidak! Sesungguhnya catatan orang-orang yang berbakti benar-benar tersimpan dalam ‘Illiyyīn.
196	6	19	وَمَآ اَدْرٰىكَ مَا عِلِّيُّوْنَۗ	Tahukah engkau apakah ‘Illiyyīn itu?
197	6	20	كِتٰبٌ مَّرْقُوْمٌۙ	(Itulah) kitab yang berisi catatan (amal)
198	6	21	يَّشْهَدُهُ الْمُقَرَّبُوْنَۗ	yang disaksikan oleh (malaikat-malaikat) yang didekatkan (kepada Allah).
199	6	22	اِنَّ الْاَبْرَارَ لَفِيْ نَعِيْمٍۙ	Sesungguhnya orang-orang yang berbakti benar-benar berada dalam (surga yang penuh) kenikmatan.
200	6	23	عَلَى الْاَرَاۤىِٕكِ يَنْظُرُوْنَۙ	Mereka (duduk) di atas dipan-dipan (sambil) melepas pandangan.
201	6	24	تَعْرِفُ فِيْ وُجُوْهِهِمْ نَضْرَةَ النَّعِيْمِۚ	Engkau dapat mengetahui pada wajah mereka gemerlapnya kenikmatan.
202	6	25	يُسْقَوْنَ مِنْ رَّحِيْقٍ مَّخْتُوْمٍۙ	Mereka diberi minum dari khamar murni (tidak memabukkan) yang (tempatnya) masih diberi lak (sebagai jaminan keasliannya).
203	6	26	خِتٰمُهٗ مِسْكٌ ۗوَفِيْ ذٰلِكَ فَلْيَتَنَافَسِ الْمُتَنٰفِسُوْنَۗ	Laknya terbuat dari kasturi. Untuk (mendapatkan) yang demikian itu hendaknya orang berlomba-lomba.
204	6	27	وَمِزَاجُهٗ مِنْ تَسْنِيْمٍۙ	Campurannya terbuat dari tasnīm,
205	6	28	عَيْنًا يَّشْرَبُ بِهَا الْمُقَرَّبُوْنَۗ	(yaitu) mata air yang diminum oleh mereka yang didekatkan (kepada Allah).
206	6	29	اِنَّ الَّذِيْنَ اَجْرَمُوْا كَانُوْا مِنَ الَّذِيْنَ اٰمَنُوْا يَضْحَكُوْنَۖ	Sesungguhnya orang-orang yang berdosa adalah mereka yang dahulu selalu mentertawakan orang-orang yang beriman.
207	6	30	وَاِذَا مَرُّوْا بِهِمْ يَتَغَامَزُوْنَۖ	Apabila mereka (orang-orang yang beriman) melintas di hadapan mereka, mereka saling mengedip-ngedipkan matanya.
208	6	31	وَاِذَا انْقَلَبُوْٓا اِلٰٓى اَهْلِهِمُ انْقَلَبُوْا فَكِهِيْنَۖ	Apabila kembali kepada kaumnya, mereka kembali dengan gembira ria (dan sombong).
209	6	32	وَاِذَا رَاَوْهُمْ قَالُوْٓا اِنَّ هٰٓؤُلَاۤءِ لَضَاۤلُّوْنَۙ	Apabila melihat (orang-orang mukmin), mereka mengatakan, “Sesungguhnya mereka benar-benar orang-orang sesat,”
210	6	33	وَمَآ اُرْسِلُوْا عَلَيْهِمْ حٰفِظِيْنَۗ	padahal mereka (orang-orang yang berdosa itu) tidak diutus sebagai penjaga (orang-orang mukmin).
211	6	34	فَالْيَوْمَ الَّذِيْنَ اٰمَنُوْا مِنَ الْكُفَّارِ يَضْحَكُوْنَۙ	Pada hari ini (hari Kiamat), orang-orang yang berimanlah yang mentertawakan orang-orang kafir.
212	6	35	عَلَى الْاَرَاۤىِٕكِ يَنْظُرُوْنَۗ	Mereka (duduk) di atas dipan-dipan (sambil) melepas pandangan.
213	6	36	هَلْ ثُوِّبَ الْكُفَّارُ مَا كَانُوْا يَفْعَلُوْنَ ࣖ	Apakah orang-orang kafir itu telah diberi balasan (hukuman) terhadap apa yang selalu mereka perbuat?
214	7	1	اِذَا السَّمَاۤءُ انْشَقَّتْۙ	Apabila langit terbelah
215	7	2	وَاَذِنَتْ لِرَبِّهَا وَحُقَّتْۙ	serta patuh kepada Tuhannya dan sudah semestinya patuh.
216	7	3	وَاِذَا الْاَرْضُ مُدَّتْۙ	Apabila bumi diratakan,
217	7	4	وَاَلْقَتْ مَا فِيْهَا وَتَخَلَّتْۙ	memuntahkan apa yang ada di dalamnya dan menjadi kosong,
218	7	5	وَاَذِنَتْ لِرَبِّهَا وَحُقَّتْۗ	serta patuh kepada Tuhannya, dan sudah semestinya patuh.
219	7	6	يٰٓاَيُّهَا الْاِنْسَانُ اِنَّكَ كَادِحٌ اِلٰى رَبِّكَ كَدْحًا فَمُلٰقِيْهِۚ	Wahai manusia, sesungguhnya engkau telah bekerja keras menuju (pertemuan dengan) Tuhanmu. Maka, engkau pasti menemui-Nya.
220	7	7	فَاَمَّا مَنْ اُوْتِيَ كِتٰبَهٗ بِيَمِيْنِهٖۙ	Adapun orang yang catatannya diberikan dari sebelah kanannya,
221	7	8	فَسَوْفَ يُحَاسَبُ حِسَابًا يَّسِيْرًاۙ	dia akan dihisab dengan pemeriksaan yang mudah
222	7	9	وَّيَنْقَلِبُ اِلٰٓى اَهْلِهٖ مَسْرُوْرًاۗ	dan dia akan kembali kepada keluarganya (yang sama-sama beriman) dengan gembira.
223	7	10	وَاَمَّا مَنْ اُوْتِيَ كِتٰبَهٗ وَرَاۤءَ ظَهْرِهٖۙ	Adapun orang yang catatannya diberikan dari belakang punggungnya,
224	7	11	فَسَوْفَ يَدْعُوْا ثُبُوْرًاۙ	dia akan berteriak, “Celakalah aku!”
225	7	12	وَّيَصْلٰى سَعِيْرًاۗ	Dia akan memasuki (neraka) Sa‘ir (yang menyala-nyala).
269	9	9	يَوْمَ تُبْلَى السَّرَاۤىِٕرُۙ	pada hari ditampakkan segala rahasia.
226	7	13	اِنَّهٗ كَانَ فِيْٓ اَهْلِهٖ مَسْرُوْرًاۗ	Sesungguhnya dia dahulu (di dunia) bergembira di kalangan keluarganya (yang sama-sama kafir).
227	7	14	اِنَّهٗ ظَنَّ اَنْ لَّنْ يَّحُوْرَ ۛ	Sesungguhnya dia mengira bahwa dia tidak akan kembali (kepada Tuhannya).
228	7	15	بَلٰىۛ اِنَّ رَبَّهٗ كَانَ بِهٖ بَصِيْرًاۗ	Tidak demikian. Sesungguhnya Tuhannya selalu melihatnya.
229	7	16	فَلَآ اُقْسِمُ بِالشَّفَقِۙ	Aku bersumpah demi cahaya merah pada waktu senja,
230	7	17	وَالَّيْلِ وَمَا وَسَقَۙ	demi malam dan apa yang diselubunginya,
231	7	18	وَالْقَمَرِ اِذَا اتَّسَقَۙ	dan demi bulan apabila jadi purnama,
232	7	19	لَتَرْكَبُنَّ طَبَقًا عَنْ طَبَقٍۗ	sungguh, kamu benar-benar akan menjalani tingkat demi tingkat (dalam kehidupan).
233	7	20	فَمَا لَهُمْ لَا يُؤْمِنُوْنَۙ	Maka, mengapa mereka tidak mau beriman?
234	7	21	وَاِذَا قُرِئَ عَلَيْهِمُ الْقُرْاٰنُ لَا يَسْجُدُوْنَ ۗ ۩	Apabila Al-Qur’an dibacakan kepada mereka, mereka tidak (mau) bersujud,
235	7	22	بَلِ الَّذِيْنَ كَفَرُوْا يُكَذِّبُوْنَۖ	bahkan orang-orang yang kufur itu mendustakan(-nya).
236	7	23	وَاللّٰهُ اَعْلَمُ بِمَا يُوْعُوْنَۖ	Allah lebih mengetahui apa yang mereka sembunyikan (dalam hati mereka).
237	7	24	فَبَشِّرْهُمْ بِعَذَابٍ اَلِيْمٍۙ	Maka, berilah mereka kabar ‘gembira’ dengan azab yang pedih,
238	7	25	اِلَّا الَّذِيْنَ اٰمَنُوْا وَعَمِلُوا الصّٰلِحٰتِ لَهُمْ اَجْرٌ غَيْرُ مَمْنُوْنٍ ࣖ	Kecuali orang-orang yang beriman dan mengerjakan kebajikan. Bagi merekalah pahala yang tidak putus-putus.
239	8	1	وَالسَّمَاۤءِ ذَاتِ الْبُرُوْجِۙ	Demi langit yang mempunyai gugusan bintang,
240	8	2	وَالْيَوْمِ الْمَوْعُوْدِۙ	demi hari yang dijanjikan,
241	8	3	وَشَاهِدٍ وَّمَشْهُوْدٍۗ	demi yang menyaksikan dan yang disaksikan,
242	8	4	قُتِلَ اَصْحٰبُ الْاُخْدُوْدِۙ	binasalah orang-orang yang membuat parit (tempat menyiksa orang mukmin)
243	8	5	النَّارِ ذَاتِ الْوَقُوْدِۙ	(yang dikobarkan) api penuh kayu bakar.
244	8	6	اِذْ هُمْ عَلَيْهَا قُعُوْدٌۙ	Ketika (itu) mereka (hanya) duduk di sekitarnya.
245	8	7	وَّهُمْ عَلٰى مَا يَفْعَلُوْنَ بِالْمُؤْمِنِيْنَ شُهُوْدٌ ۗ	Mereka menyaksikan apa yang mereka perbuat terhadap orang-orang mukmin.
246	8	8	وَمَا نَقَمُوْا مِنْهُمْ اِلَّآ اَنْ يُّؤْمِنُوْا بِاللّٰهِ الْعَزِيْزِ الْحَمِيْدِۙ	Tidaklah mereka menyiksa (membakar) orang-orang mukmin itu, kecuali karena mereka beriman kepada Allah Yang Maha Perkasa lagi Maha Terpuji,
247	8	9	الَّذِيْ لَهٗ مُلْكُ السَّمٰوٰتِ وَالْاَرْضِ ۗوَاللّٰهُ عَلٰى كُلِّ شَيْءٍ شَهِيْدٌ  ۗ	yang memiliki kerajaan langit dan bumi. Allah Maha Menyaksikan segala sesuatu.
248	8	10	اِنَّ الَّذِيْنَ فَتَنُوا الْمُؤْمِنِيْنَ وَالْمُؤْمِنٰتِ ثُمَّ لَمْ يَتُوْبُوْا فَلَهُمْ عَذَابُ جَهَنَّمَ وَلَهُمْ عَذَابُ الْحَرِيْقِۗ	Sesungguhnya, orang-orang yang menimpakan cobaan (siksa) terhadap mukmin laki-laki dan perempuan, lalu mereka tidak bertobat, mereka akan mendapat azab Jahanam dan mereka akan mendapat azab (neraka) yang membakar.
249	8	11	اِنَّ الَّذِيْنَ اٰمَنُوْا وَعَمِلُوا الصّٰلِحٰتِ لَهُمْ جَنّٰتٌ تَجْرِيْ مِنْ تَحْتِهَا الْاَنْهٰرُ ەۗ ذٰلِكَ الْفَوْزُ الْكَبِيْرُۗ	Sesungguhnya, orang-orang yang beriman dan mengerjakan kebajikan, mereka akan mendapat surga yang mengalir di bawahnya sungai-sungai. Itulah kemenangan yang besar.
250	8	12	اِنَّ بَطْشَ رَبِّكَ لَشَدِيْدٌ ۗ	Sesungguhnya azab Tuhanmu sangat keras.
251	8	13	اِنَّهٗ هُوَ يُبْدِئُ وَيُعِيْدُۚ	Sesungguhnya Dialah yang memulai (penciptaan makhluk) dan yang mengembalikan (hidup setelah mati).
252	8	14	وَهُوَ الْغَفُوْرُ الْوَدُوْدُۙ	Dialah Yang Maha Pengampun lagi Maha Pengasih,
253	8	15	ذُو الْعَرْشِ الْمَجِيْدُۙ	Pemilik ʻArasy lagi Maha Mulia,
254	8	16	فَعَّالٌ لِّمَا يُرِيْدُۗ	Maha Kuasa berbuat apa saja yang Dia kehendaki.
255	8	17	هَلْ اَتٰىكَ حَدِيْثُ الْجُنُوْدِۙ	Sudahkah sampai kepadamu berita tentang bala tentara,
256	8	18	فِرْعَوْنَ وَثَمُوْدَۗ	(yaitu bala tentara) Fir‘aun dan Samud?
257	8	19	بَلِ الَّذِيْنَ كَفَرُوْا فِيْ تَكْذِيْبٍۙ	Memang orang-orang kafir (selalu) mendustakan,
258	8	20	وَّاللّٰهُ مِنْ وَّرَاۤىِٕهِمْ مُّحِيْطٌۚ	padahal Allah mengepung dari belakang mereka.
259	8	21	بَلْ هُوَ قُرْاٰنٌ مَّجِيْدٌۙ	Bahkan, (yang didustakan itu) Al-Qur’an yang mulia
260	8	22	فِيْ لَوْحٍ مَّحْفُوْظٍ ࣖ	yang (tersimpan) dalam (tempat) yang terjaga (Lauhulmahfuz).
261	9	1	وَالسَّمَاۤءِ وَالطَّارِقِۙ	Demi langit dan yang datang pada malam hari.
262	9	2	وَمَآ اَدْرٰىكَ مَا الطَّارِقُۙ	Tahukah kamu apakah yang datang pada malam hari itu?
263	9	3	النَّجْمُ الثَّاقِبُۙ	(Itulah) bintang yang bersinar tajam.
264	9	4	اِنْ كُلُّ نَفْسٍ لَّمَّا عَلَيْهَا حَافِظٌۗ	Setiap orang pasti ada penjaganya.
265	9	5	فَلْيَنْظُرِ الْاِنْسَانُ مِمَّ خُلِقَ	Hendaklah manusia memperhatikan dari apa dia diciptakan.
266	9	6	خُلِقَ مِنْ مَّاۤءٍ دَافِقٍۙ	Dia diciptakan dari air (mani) yang memancar,
267	9	7	يَّخْرُجُ مِنْۢ بَيْنِ الصُّلْبِ وَالتَّرَاۤىِٕبِۗ	yang keluar dari antara tulang sulbi (punggung) dan tulang dada.
268	9	8	اِنَّهٗ عَلٰى رَجْعِهٖ لَقَادِرٌۗ	Sesungguhnya Dia (Allah) benar-benar kuasa untuk mengembalikannya (hidup setelah mati)
270	9	10	فَمَا لَهٗ مِنْ قُوَّةٍ وَّلَا نَاصِرٍۗ	Maka, baginya (manusia) tidak ada lagi kekuatan dan tidak (pula) ada penolong.
271	9	11	وَالسَّمَاۤءِ ذَاتِ الرَّجْعِۙ	Demi langit yang mengandung hujan
272	9	12	وَالْاَرْضِ ذَاتِ الصَّدْعِۙ	dan bumi yang memiliki rekahan (tempat tumbuhnya pepohonan),
273	9	13	اِنَّهٗ لَقَوْلٌ فَصْلٌۙ	sesungguhnya (Al-Qur’an) itu benar-benar firman pemisah (antara yang hak dan yang batil)
274	9	14	وَّمَا هُوَ بِالْهَزْلِۗ	dan ia (Al-Qur’an) sama sekali bukan perkataan senda gurau.
275	9	15	اِنَّهُمْ يَكِيْدُوْنَ كَيْدًاۙ	Sesungguhnya mereka (orang kafir) melakukan tipu daya.
276	9	16	وَّاَكِيْدُ كَيْدًاۖ	Aku pun membalasnya dengan tipu daya.
277	9	17	فَمَهِّلِ الْكٰفِرِيْنَ اَمْهِلْهُمْ رُوَيْدًا ࣖ	Maka, tangguhkanlah orang-orang kafir itu. Biarkanlah mereka sejenak (bersenang-senang).
278	10	1	سَبِّحِ اسْمَ رَبِّكَ الْاَعْلَىۙ	Sucikanlah nama Tuhanmu Yang Maha Tinggi,
279	10	2	الَّذِيْ خَلَقَ فَسَوّٰىۖ	yang menciptakan, lalu menyempurnakan (ciptaan-Nya),
280	10	3	وَالَّذِيْ قَدَّرَ فَهَدٰىۖ	yang menentukan kadar (masing-masing) dan memberi petunjuk,
281	10	4	وَالَّذِيْٓ اَخْرَجَ الْمَرْعٰىۖ	dan yang menumbuhkan (rerumputan) padang gembala,
282	10	5	فَجَعَلَهٗ غُثَاۤءً اَحْوٰىۖ	lalu menjadikannya kering kehitam-hitaman.
283	10	6	سَنُقْرِئُكَ فَلَا تَنْسٰىٓ ۖ	Kami akan membacakan (Al-Qur’an) kepadamu (Nabi Muhammad) sehingga engkau tidak akan lupa,
284	10	7	اِلَّا مَا شَاۤءَ اللّٰهُ ۗاِنَّهٗ يَعْلَمُ الْجَهْرَ وَمَا يَخْفٰىۗ	kecuali jika Allah menghendaki. Sesungguhnya Dia mengetahui yang terang dan yang tersembunyi.
285	10	8	وَنُيَسِّرُكَ لِلْيُسْرٰىۖ	Kami akan melapangkan bagimu jalan kemudahan (dalam segala urusan).
286	10	9	فَذَكِّرْ اِنْ نَّفَعَتِ الذِّكْرٰىۗ	Maka, sampaikanlah peringatan jika peringatan itu bermanfaat.
287	10	10	سَيَذَّكَّرُ مَنْ يَّخْشٰىۙ	Orang yang takut (kepada Allah) akan mengambil pelajaran,
288	10	11	وَيَتَجَنَّبُهَا الْاَشْقَىۙ	sedangkan orang-orang yang celaka (kafir) akan menjauhinya,
289	10	12	الَّذِيْ يَصْلَى النَّارَ الْكُبْرٰىۚ	(yaitu) orang yang akan memasuki api (neraka) yang besar.
290	10	13	ثُمَّ لَا يَمُوْتُ فِيْهَا وَلَا يَحْيٰىۗ	Selanjutnya, dia tidak mati dan tidak (pula) hidup di sana.
291	10	14	قَدْ اَفْلَحَ مَنْ تَزَكّٰىۙ	Sungguh, beruntung orang yang menyucikan diri (dari kekafiran)
292	10	15	وَذَكَرَ اسْمَ رَبِّهٖ فَصَلّٰىۗ	dan mengingat nama Tuhannya, lalu dia salat.
293	10	16	بَلْ تُؤْثِرُوْنَ الْحَيٰوةَ الدُّنْيَاۖ	Adapun kamu (orang-orang kafir) mengutamakan kehidupan dunia,
294	10	17	وَالْاٰخِرَةُ خَيْرٌ وَّاَبْقٰىۗ	padahal kehidupan akhirat itu lebih baik dan lebih kekal.
295	10	18	اِنَّ هٰذَا لَفِى الصُّحُفِ الْاُوْلٰىۙ	Sesungguhnya (penjelasan) ini terdapat dalam suhuf (lembaran-lembaran) yang terdahulu,
296	10	19	صُحُفِ اِبْرٰهِيْمَ وَمُوْسٰى ࣖ	(yaitu) suhuf (yang diturunkan kepada) Ibrahim dan Musa.
297	11	1	هَلْ اَتٰىكَ حَدِيْثُ الْغَاشِيَةِۗ	Sudahkah sampai kepadamu berita tentang al-Gāsyiyah (hari Kiamat yang menutupi kesadaran manusia dengan kedahsyatannya)?
298	11	2	وُجُوْهٌ يَّوْمَىِٕذٍ خَاشِعَةٌۙ	Pada hari itu banyak wajah yang tertunduk hina.
299	11	3	عَامِلَةٌ نَّاصِبَةٌۙ	(karena) berusaha keras (menghindari azab neraka) lagi kepayahan (karena dibelenggu).
300	11	4	تَصْلٰى نَارًا حَامِيَةًۙ	Mereka memasuki api (neraka) yang sangat panas.
301	11	5	تُسْقٰى مِنْ عَيْنٍ اٰنِيَةٍۗ	(Mereka) diberi minum dari sumber mata air yang sangat panas.
302	11	6	لَيْسَ لَهُمْ طَعَامٌ اِلَّا مِنْ ضَرِيْعٍۙ	Tidak ada makanan bagi mereka selain dari pohon yang berduri.
303	11	7	لَّا يُسْمِنُ وَلَا يُغْنِيْ مِنْ جُوْعٍۗ	yang tidak menggemukkan dan tidak pula menghilangkan lapar.
304	11	8	وُجُوْهٌ يَّوْمَىِٕذٍ نَّاعِمَةٌۙ	Pada hari itu banyak (pula) wajah yang berseri-seri.
305	11	9	لِّسَعْيِهَا رَاضِيَةٌۙ	merasa puas karena usahanya.
306	11	10	فِيْ جَنَّةٍ عَالِيَةٍۙ	(Mereka) dalam surga yang tinggi.
307	11	11	لَّا تَسْمَعُ فِيْهَا لَاغِيَةًۗ	Di sana kamu tidak mendengar (perkataan) yang tidak berguna.
308	11	12	فِيْهَا عَيْنٌ جَارِيَةٌۘ	Di sana ada mata air yang mengalir.
309	11	13	فِيْهَا سُرُرٌ مَّرْفُوْعَةٌۙ	Di sana ada (pula) dipan-dipan yang ditinggikan.
310	11	14	وَّاَكْوَابٌ مَّوْضُوْعَةٌۙ	gelas-gelas yang tersedia (di dekatnya).
311	11	15	وَّنَمَارِقُ مَصْفُوْفَةٌۙ	bantal-bantal sandaran yang tersusun.
312	11	16	وَّزَرَابِيُّ مَبْثُوْثَةٌۗ	dan permadani-permadani yang terhampar.
313	11	17	اَفَلَا يَنْظُرُوْنَ اِلَى الْاِبِلِ كَيْفَ خُلِقَتْۗ	Tidakkah mereka memperhatikan unta, bagaimana ia diciptakan?
314	11	18	وَاِلَى السَّمَاۤءِ كَيْفَ رُفِعَتْۗ	Bagaimana langit ditinggikan?
315	11	19	وَاِلَى الْجِبَالِ كَيْفَ نُصِبَتْۗ	Bagaimana gunung-gunung ditegakkan?
316	11	20	وَاِلَى الْاَرْضِ كَيْفَ سُطِحَتْۗ	Bagaimana pula bumi dihamparkan?
317	11	21	فَذَكِّرْۗ اِنَّمَآ اَنْتَ مُذَكِّرٌۙ	Maka, berilah peringatan karena sesungguhnya engkau (Nabi Muhammad) hanyalah pemberi peringatan.
318	11	22	لَّسْتَ عَلَيْهِمْ بِمُصَيْطِرٍۙ	Engkau bukanlah orang yang berkuasa atas mereka.
409	16	1	وَالضُّحٰىۙ	Demi waktu duha
319	11	23	اِلَّا مَنْ تَوَلّٰى وَكَفَرَۙ	Akan tetapi, orang yang berpaling dan kufur.
320	11	24	فَيُعَذِّبُهُ اللّٰهُ الْعَذَابَ الْاَكْبَرَۗ	Allah akan mengazabnya dengan azab yang paling besar.
321	11	25	اِنَّ اِلَيْنَآ اِيَابَهُمْ	Sesungguhnya kepada Kamilah mereka kembali.
322	11	26	ثُمَّ اِنَّ عَلَيْنَا حِسَابَهُمْࣖ	Kemudian, sesungguhnya Kamilah yang berhak melakukan hisab (perhitungan) atas mereka.
323	12	1	وَالْفَجْرِۙ	Demi waktu fajar,
324	12	2	وَلَيَالٍ عَشْرٍۙ	demi malam yang sepuluh,
325	12	3	وَّالشَّفْعِ وَالْوَتْرِۙ	demi yang genap dan yang ganjil,
326	12	4	وَالَّيْلِ اِذَا يَسْرِۚ	dan demi malam apabila berlalu.
327	12	5	هَلْ فِيْ ذٰلِكَ قَسَمٌ لِّذِيْ حِجْرٍۗ	Apakah pada yang demikian itu terdapat sumpah (yang dapat diterima) oleh (orang) yang berakal?
328	12	6	اَلَمْ تَرَ كَيْفَ فَعَلَ رَبُّكَ بِعَادٍۖ	Tidakkah engkau (Nabi Muhammad) memperhatikan bagaimana Tuhanmu berbuat terhadap (kaum) ‘Ad,
329	12	7	اِرَمَ ذَاتِ الْعِمَادِۖ	(yaitu) penduduk Iram (ibu kota kaum ‘Ad) yang mempunyai bangunan-bangunan yang tinggi
330	12	8	الَّتِيْ لَمْ يُخْلَقْ مِثْلُهَا فِى الْبِلَادِۖ	yang sebelumnya tidak pernah dibangun (suatu kota pun) seperti itu di negeri-negeri (lain)?
331	12	9	وَثَمُوْدَ الَّذِيْنَ جَابُوا الصَّخْرَ بِالْوَادِۖ	(Tidakkah engkau perhatikan pula kaum) Samud yang memotong batu-batu besar di lembah
332	12	10	وَفِرْعَوْنَ ذِى الْاَوْتَادِۖ	dan Fir‘aun yang mempunyai pasak-pasak (bangunan yang besar)
333	12	11	الَّذِيْنَ طَغَوْا فِى الْبِلَادِۖ	yang berbuat sewenang-wenang dalam negeri,
334	12	12	فَاَكْثَرُوْا فِيْهَا الْفَسَادَۖ	lalu banyak berbuat kerusakan di dalamnya (negeri itu),
335	12	13	فَصَبَّ عَلَيْهِمْ رَبُّكَ سَوْطَ عَذَابٍۖ	maka Tuhanmu menimpakan cemeti azab (yang dahsyat) kepada mereka?
336	12	14	اِنَّ رَبَّكَ لَبِالْمِرْصَادِۗ	Sesungguhnya Tuhanmu benar-benar mengawasi.
337	12	15	فَاَمَّا الْاِنْسَانُ اِذَا مَا ابْتَلٰىهُ رَبُّهٗ فَاَكْرَمَهٗ وَنَعَّمَهٗۙ فَيَقُوْلُ رَبِّيْٓ اَكْرَمَنِۗ	Adapun manusia, apabila Tuhan mengujinya lalu memuliakannya dan memberinya kenikmatan, berkatalah dia, “Tuhanku telah memuliakanku.”
338	12	16	وَاَمَّآ اِذَا مَا ابْتَلٰىهُ فَقَدَرَ عَلَيْهِ رِزْقَهٗ ەۙ فَيَقُوْلُ رَبِّيْٓ اَهَانَنِۚ	Sementara itu, apabila Dia mengujinya lalu membatasi rezekinya, berkatalah dia, “Tuhanku telah menghinaku.”
339	12	17	كَلَّا بَلْ لَّا تُكْرِمُوْنَ الْيَتِيْمَۙ	Sekali-kali tidak! Sebaliknya, kamu tidak memuliakan anak yatim,
340	12	18	وَلَا تَحٰۤضُّوْنَ عَلٰى طَعَامِ الْمِسْكِيْنِۙ	tidak saling mengajak memberi makan orang miskin,
341	12	19	وَتَأْكُلُوْنَ التُّرَاثَ اَكْلًا لَّمًّاۙ	memakan harta warisan dengan cara mencampurbaurkan (yang halal dan yang haram),
342	12	20	وَّتُحِبُّوْنَ الْمَالَ حُبًّا جَمًّاۗ	dan mencintai harta dengan kecintaan yang berlebihan.
343	12	21	كَلَّآ اِذَا دُكَّتِ الْاَرْضُ دَكًّا دَكًّاۙ	Jangan sekali-kali begitu! Apabila bumi diguncangkan berturut-turut (berbenturan),
344	12	22	وَّجَاۤءَ رَبُّكَ وَالْمَلَكُ صَفًّا صَفًّاۚ	Tuhanmu datang, begitu pula para malaikat (yang datang) berbaris-baris,
345	12	23	وَجِايْۤءَ يَوْمَىِٕذٍۢ بِجَهَنَّمَۙ يَوْمَىِٕذٍ يَّتَذَكَّرُ الْاِنْسَانُ وَاَنّٰى لَهُ الذِّكْرٰىۗ	dan pada hari itu (neraka) Jahanam didatangkan, sadarlah manusia pada hari itu juga. Akan tetapi, bagaimana bisa kesadaran itu bermanfaat baginya?
346	12	24	يَقُوْلُ يٰلَيْتَنِيْ قَدَّمْتُ لِحَيَاتِيْۚ	Dia berkata, “Oh, seandainya dahulu aku mengerjakan (kebajikan) untuk hidupku ini!”
347	12	25	فَيَوْمَىِٕذٍ لَّا يُعَذِّبُ عَذَابَهٗٓ اَحَدٌ ۙ	Pada hari itu tidak ada seorang pun yang mampu mengazab (seadil) azab-Nya.
348	12	26	وَّلَا يُوْثِقُ وَثَاقَهٗٓ اَحَدٌ ۗ	Tidak ada seorang pun juga yang mampu mengikat (sekuat) ikatan-Nya.
349	12	27	يٰٓاَيَّتُهَا النَّفْسُ الْمُطْمَىِٕنَّةُۙ	Wahai jiwa yang tenang,
350	12	28	ارْجِعِيْٓ اِلٰى رَبِّكِ رَاضِيَةً مَّرْضِيَّةً ۚ	kembalilah kepada Tuhanmu dengan rida dan diridai.
351	12	29	فَادْخُلِيْ فِيْ عِبٰدِيْۙ	Lalu, masuklah ke dalam golongan hamba-hamba-Ku
352	12	30	وَادْخُلِيْ جَنَّتِيْ ࣖࣖ	dan masuklah ke dalam surga-Ku!
353	13	1	لَآ اُقْسِمُ بِهٰذَا الْبَلَدِۙ	Aku bersumpah demi negeri ini (Makkah),
354	13	2	وَاَنْتَ حِلٌّۢ بِهٰذَا الْبَلَدِۙ	sedangkan engkau (Nabi Muhammad) bertempat tinggal di negeri (Makkah) ini.
355	13	3	وَوَالِدٍ وَّمَا وَلَدَۙ	(Aku juga bersumpah) demi bapak dan anaknya,
356	13	4	لَقَدْ خَلَقْنَا الْاِنْسَانَ فِيْ كَبَدٍۗ	Sungguh, Kami benar-benar telah menciptakan manusia dalam keadaan susah payah.
357	13	5	اَيَحْسَبُ اَنْ لَّنْ يَّقْدِرَ عَلَيْهِ اَحَدٌ ۘ	Apakah dia (manusia) itu mengira bahwa tidak ada seorang pun yang berkuasa atasnya?
358	13	6	يَقُوْلُ اَهْلَكْتُ مَالًا لُّبَدًاۗ	Dia mengatakan, “Aku telah menghabiskan harta yang banyak.”
359	13	7	اَيَحْسَبُ اَنْ لَّمْ يَرَهٗٓ اَحَدٌۗ	Apakah dia mengira bahwa tidak ada seorang pun yang melihatnya?
360	13	8	اَلَمْ نَجْعَلْ لَّهٗ عَيْنَيْنِۙ	Bukankah Kami telah menjadikan untuknya sepasang mata,
361	13	9	وَلِسَانًا وَّشَفَتَيْنِۙ	lidah, dan sepasang bibir,
410	16	2	وَالَّيْلِ اِذَا سَجٰىۙ	dan demi waktu malam apabila telah sunyi,
362	13	10	وَهَدَيْنٰهُ النَّجْدَيْنِۙ	serta Kami juga telah menunjukkan kepadanya dua jalan (kebajikan dan kejahatan)?
363	13	11	فَلَا اقْتَحَمَ الْعَقَبَةَ ۖ	Maka, tidakkah sebaiknya dia menempuh jalan (kebajikan) yang mendaki dan sukar?
364	13	12	وَمَآ اَدْرٰىكَ مَا الْعَقَبَةُ ۗ	Tahukah kamu apakah jalan yang mendaki dan sukar itu?
365	13	13	فَكُّ رَقَبَةٍۙ	(Itulah upaya) melepaskan perbudakan
366	13	14	اَوْ اِطْعَامٌ فِيْ يَوْمٍ ذِيْ مَسْغَبَةٍۙ	atau memberi makan pada hari terjadi kelaparan
367	13	15	يَّتِيْمًا ذَا مَقْرَبَةٍۙ	(kepada) anak yatim yang memiliki hubungan kekerabatan
368	13	16	اَوْ مِسْكِيْنًا ذَا مَتْرَبَةٍۗ	atau orang miskin yang sangat membutuhkan.
369	13	17	ثُمَّ كَانَ مِنَ الَّذِيْنَ اٰمَنُوْا وَتَوَاصَوْا بِالصَّبْرِ وَتَوَاصَوْا بِالْمَرْحَمَةِۗ	Kemudian, dia juga termasuk orang-orang yang beriman dan saling berpesan untuk bersabar serta saling berpesan untuk berkasih sayang.
370	13	18	اُولٰۤىِٕكَ اَصْحٰبُ الْمَيْمَنَةِۗ	Mereka itulah golongan kanan.
371	13	19	وَالَّذِيْنَ كَفَرُوْا بِاٰيٰتِنَا هُمْ اَصْحٰبُ الْمَشْـَٔمَةِۗ	Adapun orang-orang yang kufur pada ayat-ayat Kami, merekalah golongan kiri.
372	13	20	عَلَيْهِمْ نَارٌ مُّؤْصَدَةٌ ࣖ	Mereka berada dalam neraka yang ditutup rapat.
373	14	1	وَالشَّمْسِ وَضُحٰىهَاۖ	Demi matahari dan sinarnya pada waktu duha (ketika matahari naik sepenggalah),
374	14	2	وَالْقَمَرِ اِذَا تَلٰىهَاۖ	demi bulan saat mengiringinya,
375	14	3	وَالنَّهَارِ اِذَا جَلّٰىهَاۖ	demi siang saat menampakkannya,
376	14	4	وَالَّيْلِ اِذَا يَغْشٰىهَاۖ	demi malam saat menutupinya (gelap gulita),
377	14	5	وَالسَّمَاۤءِ وَمَا بَنٰىهَاۖ	demi langit serta pembuatannya,
378	14	6	وَالْاَرْضِ وَمَا طَحٰىهَاۖ	demi bumi serta penghamparannya,
379	14	7	وَنَفْسٍ وَّمَا سَوّٰىهَاۖ	dan demi jiwa serta penyempurnaan (ciptaan)-nya,
380	14	8	فَاَلْهَمَهَا فُجُوْرَهَا وَتَقْوٰىهَاۖ	lalu Dia mengilhamkan kepadanya (jalan) kejahatan dan ketakwaannya,
381	14	9	قَدْ اَفْلَحَ مَنْ زَكّٰىهَاۖ	Sungguh beruntung orang yang menyucikannya (jiwa itu).
382	14	10	وَقَدْ خَابَ مَنْ دَسّٰىهَاۗ	dan sungguh rugi orang yang mengotorinya.
383	14	11	كَذَّبَتْ ثَمُوْدُ بِطَغْوٰىهَآ ۖ	(Kaum) Samud telah mendustakan (rasulnya) karena mereka melampaui batas.
384	14	12	اِذِ انْۢبَعَثَ اَشْقٰىهَاۖ	ketika orang yang paling celaka di antara mereka bangkit (untuk menyembelih unta betina Allah).
385	14	13	فَقَالَ لَهُمْ رَسُوْلُ اللّٰهِ نَاقَةَ اللّٰهِ وَسُقْيٰهَاۗ	Rasul Allah (Saleh) lalu berkata kepada mereka, “(Biarkanlah) unta betina Allah ini beserta minumannya.”
386	14	14	فَكَذَّبُوْهُ فَعَقَرُوْهَاۖ فَدَمْدَمَ عَلَيْهِمْ رَبُّهُمْ بِذَنْۢبِهِمْ فَسَوّٰىهَاۖ	Namun, mereka kemudian mendustakannya (Saleh) dan menyembelih (unta betina) itu. Maka, Tuhan membinasakan mereka karena dosa-dosanya, lalu meratakan mereka (dengan tanah).
387	14	15	وَلَا يَخَافُ عُقْبٰهَا ࣖ	Dia tidak takut terhadap akibatnya.
388	15	1	وَالَّيْلِ اِذَا يَغْشٰىۙ	Demi malam apabila menutupi (cahaya siang),
389	15	2	وَالنَّهَارِ اِذَا تَجَلّٰىۙ	demi siang apabila terang benderang,
390	15	3	وَمَا خَلَقَ الذَّكَرَ وَالْاُنْثٰىٓ ۙ	dan demi penciptaan laki-laki dan perempuan,
391	15	4	اِنَّ سَعْيَكُمْ لَشَتّٰىۗ	sesungguhnya usahamu benar-benar beraneka ragam.
392	15	5	فَاَمَّا مَنْ اَعْطٰى وَاتَّقٰىۙ	Siapa yang memberikan (hartanya di jalan Allah) dan bertakwa
393	15	6	وَصَدَّقَ بِالْحُسْنٰىۙ	serta membenarkan adanya (balasan) yang terbaik (surga),
394	15	7	فَسَنُيَسِّرُهٗ لِلْيُسْرٰىۗ	Kami akan melapangkan baginya jalan kemudahan (kebahagiaan).
395	15	8	وَاَمَّا مَنْۢ بَخِلَ وَاسْتَغْنٰىۙ	Adapun orang yang kikir dan merasa dirinya cukup (tidak perlu pertolongan Allah)
396	15	9	وَكَذَّبَ بِالْحُسْنٰىۙ	serta mendustakan (balasan) yang terbaik,
397	15	10	فَسَنُيَسِّرُهٗ لِلْعُسْرٰىۗ	Kami akan memudahkannya menuju jalan kesengsaraan.
398	15	11	وَمَا يُغْنِيْ عَنْهُ مَالُهٗٓ اِذَا تَرَدّٰىٓۙ	Hartanya tidak bermanfaat baginya apabila dia telah binasa.
399	15	12	اِنَّ عَلَيْنَا لَلْهُدٰىۖ	Sesungguhnya Kamilah yang (berhak) memberi petunjuk.
400	15	13	وَاِنَّ لَنَا لَلْاٰخِرَةَ وَالْاُوْلٰىۗ	Sesungguhnya milik Kamilah akhirat dan dunia.
401	15	14	فَاَنْذَرْتُكُمْ نَارًا تَلَظّٰىۚ	Aku memperingatkanmu dengan neraka yang menyala-nyala.
402	15	15	لَا يَصْلٰىهَآ اِلَّا الْاَشْقَىۙ	Tidak masuk ke dalamnya kecuali orang yang paling celaka,
403	15	16	الَّذِيْ كَذَّبَ وَتَوَلّٰىۗ	yang mendustakan (kebenaran) dan berpaling (dari keimanan).
404	15	17	وَسَيُجَنَّبُهَا الْاَتْقَىۙ	Akan dijauhkan darinya (neraka) orang yang paling bertakwa,
405	15	18	الَّذِيْ يُؤْتِيْ مَالَهٗ يَتَزَكّٰىۚ	yang menginfakkan hartanya (di jalan Allah) untuk membersihkan (diri dari sifat kikir dan tamak).
406	15	19	وَمَا لِاَحَدٍ عِنْدَهٗ مِنْ نِّعْمَةٍ تُجْزٰىٓۙ	Tidak ada suatu nikmat pun yang diberikan seseorang kepadanya yang harus dibalas,
407	15	20	اِلَّا ابْتِغَاۤءَ وَجْهِ رَبِّهِ الْاَعْلٰىۚ	kecuali (dia memberikannya semata-mata) karena mencari keridaan Tuhannya Yang Maha Tinggi.
408	15	21	وَلَسَوْفَ يَرْضٰى ࣖ	Sungguh, kelak dia akan mendapatkan kepuasan (menerima balasan amalnya).
411	16	3	مَا وَدَّعَكَ رَبُّكَ وَمَا قَلٰىۗ	Tuhanmu (Nabi Muhammad) tidak meninggalkan dan tidak (pula) membencimu.
412	16	4	وَلَلْاٰخِرَةُ خَيْرٌ لَّكَ مِنَ الْاُوْلٰىۗ	Sungguh, akhirat itu lebih baik bagimu daripada yang permulaan (dunia).
413	16	5	وَلَسَوْفَ يُعْطِيْكَ رَبُّكَ فَتَرْضٰىۗ	Sungguh, kelak (di akhirat nanti) Tuhanmu pasti memberikan karunia-Nya kepadamu sehingga engkau rida.
414	16	6	اَلَمْ يَجِدْكَ يَتِيْمًا فَاٰوٰىۖ	Bukankah Dia mendapatimu sebagai seorang yatim, lalu Dia melindungi(-mu);
415	16	7	وَوَجَدَكَ ضَاۤلًّا فَهَدٰىۖ	mendapatimu sebagai seorang yang tidak tahu (tentang syariat), lalu Dia memberimu petunjuk (wahyu);
416	16	8	وَوَجَدَكَ عَاۤىِٕلًا فَاَغْنٰىۗ	dan mendapatimu sebagai seorang yang fakir, lalu Dia memberimu kecukupan?
417	16	9	فَاَمَّا الْيَتِيْمَ فَلَا تَقْهَرْۗ	Terhadap anak yatim, janganlah engkau berlaku sewenang-wenang.
418	16	10	وَاَمَّا السَّاۤىِٕلَ فَلَا تَنْهَرْ	Terhadap orang yang meminta-minta, janganlah engkau menghardik.
419	16	11	وَاَمَّا بِنِعْمَةِ رَبِّكَ فَحَدِّثْ ࣖ	Terhadap nikmat Tuhanmu, nyatakanlah (dengan bersyukur).
420	17	1	اَلَمْ نَشْرَحْ لَكَ صَدْرَكَۙ	Bukankah Kami telah melapangkan dadamu (Nabi Muhammad),
421	17	2	وَوَضَعْنَا عَنْكَ وِزْرَكَۙ	meringankan beban (tugas-tugas kenabian) darimu
422	17	3	الَّذِيْٓ اَنْقَضَ ظَهْرَكَۙ	yang memberatkan punggungmu,
423	17	4	وَرَفَعْنَا لَكَ ذِكْرَكَۗ	dan meninggikan (derajat)-mu (dengan selalu) menyebut-nyebut (nama)-mu?
424	17	5	فَاِنَّ مَعَ الْعُسْرِ يُسْرًاۙ	Maka, sesungguhnya beserta kesulitan ada kemudahan.
425	17	6	اِنَّ مَعَ الْعُسْرِ يُسْرًاۗ	Sesungguhnya beserta kesulitan ada kemudahan.
426	17	7	فَاِذَا فَرَغْتَ فَانْصَبْۙ	Apabila engkau telah selesai (dengan suatu kebajikan), teruslah bekerja keras (untuk kebajikan yang lain)
427	17	8	وَاِلٰى رَبِّكَ فَارْغَبْ ࣖ	dan hanya kepada Tuhanmu berharaplah!
428	18	1	وَالتِّيْنِ وَالزَّيْتُوْنِۙ	Demi (buah) tin dan (buah) zaitun,
429	18	2	وَطُوْرِ سِيْنِيْنَۙ	demi gunung Sinai,
430	18	3	وَهٰذَا الْبَلَدِ الْاَمِيْنِۙ	dan demi negeri (Makkah) yang aman ini,
431	18	4	لَقَدْ خَلَقْنَا الْاِنْسَانَ فِيْٓ اَحْسَنِ تَقْوِيْمٍۖ	sungguh, Kami benar-benar telah menciptakan manusia dalam bentuk yang sebaik-baiknya.
432	18	5	ثُمَّ رَدَدْنٰهُ اَسْفَلَ سٰفِلِيْنَۙ	Kemudian, kami kembalikan dia ke tempat yang serendah-rendahnya,
433	18	6	اِلَّا الَّذِيْنَ اٰمَنُوْا وَعَمِلُوا الصّٰلِحٰتِ فَلَهُمْ اَجْرٌ غَيْرُ مَمْنُوْنٍۗ	kecuali orang-orang yang beriman dan mengerjakan kebajikan. Maka, mereka akan mendapat pahala yang tidak putus-putusnya.
434	18	7	فَمَا يُكَذِّبُكَ بَعْدُ بِالدِّيْنِۗ	Maka, apa alasanmu (wahai orang kafir) mendustakan hari Pembalasan setelah (adanya bukti-bukti) itu?
435	18	8	اَلَيْسَ اللّٰهُ بِاَحْكَمِ الْحٰكِمِيْنَ ࣖ	Bukankah Allah hakim yang paling adil?
436	19	1	اِقْرَأْ بِاسْمِ رَبِّكَ الَّذِيْ خَلَقَۚ	Bacalah dengan (menyebut) nama Tuhanmu yang menciptakan!
437	19	2	خَلَقَ الْاِنْسَانَ مِنْ عَلَقٍۚ	Dia menciptakan manusia dari segumpal darah.
438	19	3	اِقْرَأْ وَرَبُّكَ الْاَكْرَمُۙ	Bacalah! Tuhanmulah Yang Maha Mulia,
439	19	4	الَّذِيْ عَلَّمَ بِالْقَلَمِۙ	yang mengajar (manusia) dengan pena.
440	19	5	عَلَّمَ الْاِنْسَانَ مَا لَمْ يَعْلَمْۗ	Dia mengajarkan manusia apa yang tidak diketahuinya.
441	19	6	كَلَّآ اِنَّ الْاِنْسَانَ لَيَطْغٰىٓ ۙ	Sekali-kali tidak! Sesungguhnya manusia itu benar-benar melampaui batas
442	19	7	اَنْ رَّاٰهُ اسْتَغْنٰىۗ	ketika melihat dirinya serba berkecukupan.
443	19	8	اِنَّ اِلٰى رَبِّكَ الرُّجْعٰىۗ	Sesungguhnya hanya kepada Tuhanmulah tempat kembali(-mu).
444	19	9	اَرَاَيْتَ الَّذِيْ يَنْهٰىۙ	Tahukah kamu tentang orang yang melarang
445	19	10	عَبْدًا اِذَا صَلّٰىۗ	seorang hamba ketika dia melaksanakan salat?
446	19	11	اَرَاَيْتَ اِنْ كَانَ عَلَى الْهُدٰىٓۙ	Bagaimana pendapatmu kalau terbukti dia berada di dalam kebenaran
447	19	12	اَوْ اَمَرَ بِالتَّقْوٰىۗ	atau dia menyuruh bertakwa (kepada Allah)?
448	19	13	اَرَاَيْتَ اِنْ كَذَّبَ وَتَوَلّٰىۗ	Bagaimana pendapatmu kalau dia mendustakan (kebenaran) dan berpaling (dari keimanan)?
449	19	14	اَلَمْ يَعْلَمْ بِاَنَّ اللّٰهَ يَرٰىۗ	Tidakkah dia mengetahui bahwa sesungguhnya Allah melihat (segala perbuatannya)?
450	19	15	كَلَّا لَىِٕنْ لَّمْ يَنْتَهِ ەۙ لَنَسْفَعًاۢ بِالنَّاصِيَةِۙ	Sekali-kali tidak! Sungguh, jika dia tidak berhenti (berbuat demikian), niscaya Kami tarik ubun-ubunnya (ke dalam neraka),
451	19	16	نَاصِيَةٍ كَاذِبَةٍ خَاطِئَةٍۚ	(yaitu) ubun-ubun orang yang mendustakan (kebenaran) dan durhaka.
452	19	17	فَلْيَدْعُ نَادِيَهٗۙ	Biarlah dia memanggil golongannya (untuk menolongnya).
453	19	18	سَنَدْعُ الزَّبَانِيَةَۙ	Kelak Kami akan memanggil (Malaikat) Zabaniah (penyiksa orang-orang yang berdosa).
454	19	19	كَلَّاۗ لَا تُطِعْهُ وَاسْجُدْ وَاقْتَرِبْ ۩ ࣖ	Sekali-kali tidak! Janganlah patuh kepadanya, (tetapi) sujud dan mendekatlah (kepada Allah).
455	20	1	اِنَّآ اَنْزَلْنٰهُ فِيْ لَيْلَةِ الْقَدْرِ	Sesungguhnya Kami telah menurunkannya (Al-Qur’an) pada Lailatulqadar.
456	20	2	وَمَآ اَدْرٰىكَ مَا لَيْلَةُ الْقَدْرِۗ	Tahukah kamu apakah Lailatulqadar itu?
457	20	3	لَيْلَةُ الْقَدْرِ ەۙ خَيْرٌ مِّنْ اَلْفِ شَهْرٍۗ	Lailatulqadar itu lebih baik daripada seribu bulan.
458	20	4	تَنَزَّلُ الْمَلٰۤىِٕكَةُ وَالرُّوْحُ فِيْهَا بِاِذْنِ رَبِّهِمْۚ مِنْ كُلِّ اَمْرٍۛ	Pada malam itu turun para malaikat dan Rūḥ (Jibril) dengan izin Tuhannya untuk mengatur semua urusan.
459	20	5	سَلٰمٌ ۛهِيَ حَتّٰى مَطْلَعِ الْفَجْرِ ࣖ	Sejahteralah (malam) itu sampai terbit fajar.
460	21	1	لَمْ يَكُنِ الَّذِيْنَ كَفَرُوْا مِنْ اَهْلِ الْكِتٰبِ وَالْمُشْرِكِيْنَ مُنْفَكِّيْنَ حَتّٰى تَأْتِيَهُمُ الْبَيِّنَةُۙ	Orang-orang yang kufur dari golongan Ahlulkitab dan orang-orang musyrik tidak akan meninggalkan (kekufuran mereka) sampai datang kepada mereka bukti yang nyata,
461	21	2	رَسُوْلٌ مِّنَ اللّٰهِ يَتْلُوْا صُحُفًا مُّطَهَّرَةًۙ	(yaitu) seorang Rasul dari Allah (Nabi Muhammad) yang membacakan lembaran-lembaran suci (Al-Qur’an)
462	21	3	فِيْهَا كُتُبٌ قَيِّمَةٌ ۗ	yang di dalamnya terdapat (isi) kitab-kitab yang lurus (benar).
463	21	4	وَمَا تَفَرَّقَ الَّذِيْنَ اُوْتُوا الْكِتٰبَ اِلَّا مِنْۢ بَعْدِ مَا جَاۤءَتْهُمُ الْبَيِّنَةُ ۗ	Tidaklah terpecah-belah orang-orang Ahlulkitab, melainkan setelah datang kepada mereka bukti yang nyata.
464	21	5	وَمَآ اُمِرُوْٓا اِلَّا لِيَعْبُدُوا اللّٰهَ مُخْلِصِيْنَ لَهُ الدِّيْنَ ەۙ حُنَفَاۤءَ وَيُقِيْمُوا الصَّلٰوةَ وَيُؤْتُوا الزَّكٰوةَ وَذٰلِكَ دِيْنُ الْقَيِّمَةِۗ	Mereka tidak diperintah, kecuali untuk menyembah Allah dengan mengikhlaskan ketaatan kepada-Nya lagi hanif (istikamah), melaksanakan salat, dan menunaikan zakat. Itulah agama yang lurus (benar).
465	21	6	اِنَّ الَّذِيْنَ كَفَرُوْا مِنْ اَهْلِ الْكِتٰبِ وَالْمُشْرِكِيْنَ فِيْ نَارِ جَهَنَّمَ خٰلِدِيْنَ فِيْهَاۗ اُولٰۤىِٕكَ هُمْ شَرُّ الْبَرِيَّةِۗ	Sesungguhnya orang-orang yang kufur dari golongan Ahlulkitab dan orang-orang musyrik (akan masuk) neraka Jahanam. Mereka kekal di dalamnya. Mereka itulah seburuk-buruk makhluk.
466	21	7	اِنَّ الَّذِيْنَ اٰمَنُوْا وَعَمِلُوا الصّٰلِحٰتِ اُولٰۤىِٕكَ هُمْ خَيْرُ الْبَرِيَّةِۗ	Sesungguhnya orang-orang yang beriman dan mengerjakan kebajikan, mereka itulah sebaik-baik makhluk.
467	21	8	جَزَاۤؤُهُمْ عِنْدَ رَبِّهِمْ جَنّٰتُ عَدْنٍ تَجْرِيْ مِنْ تَحْتِهَا الْاَنْهٰرُ خٰلِدِيْنَ فِيْهَآ اَبَدًا ۗرَضِيَ اللّٰهُ عَنْهُمْ وَرَضُوْا عَنْهُ ۗ ذٰلِكَ لِمَنْ خَشِيَ رَبَّهٗ ࣖ	Balasan mereka di sisi Tuhannya adalah surga ‘Adn yang mengalir di bawahnya sungai-sungai. Mereka kekal di dalamnya selama-lamanya. Allah rida terhadap mereka dan mereka pun rida kepada-Nya. Itu adalah (balasan) bagi orang yang takut kepada Tuhannya.
468	22	1	اِذَا زُلْزِلَتِ الْاَرْضُ زِلْزَالَهَاۙ	Apabila bumi diguncangkan dengan guncangan yang dahsyat,
469	22	2	وَاَخْرَجَتِ الْاَرْضُ اَثْقَالَهَاۙ	bumi mengeluarkan isi perutnya,
470	22	3	وَقَالَ الْاِنْسَانُ مَا لَهَاۚ	dan manusia bertanya, “Apa yang terjadi dengannya (bumi)?”
471	22	4	يَوْمَىِٕذٍ تُحَدِّثُ اَخْبَارَهَاۙ	Pada hari itu (bumi) menyampaikan berita (tentang apa yang diperbuat manusia di atasnya)
472	22	5	بِاَنَّ رَبَّكَ اَوْحٰى لَهَاۗ	karena sesungguhnya Tuhanmu telah memerintahkan (yang demikian itu) kepadanya.
473	22	6	يَوْمَىِٕذٍ يَّصْدُرُ النَّاسُ اَشْتَاتًا ەۙ لِّيُرَوْا اَعْمَالَهُمْۗ	Pada hari itu manusia keluar (dari kuburnya) dalam keadaan terpencar untuk diperlihatkan kepada mereka (balasan) semua perbuatan mereka.
474	22	7	فَمَنْ يَّعْمَلْ مِثْقَالَ ذَرَّةٍ خَيْرًا يَّرَهٗۚ	Siapa yang mengerjakan kebaikan seberat zarah, dia akan melihat (balasan)-nya.
475	22	8	وَمَنْ يَّعْمَلْ مِثْقَالَ ذَرَّةٍ شَرًّا يَّرَهٗ ࣖ	Siapa yang mengerjakan kejahatan seberat zarah, dia akan melihat (balasan)-nya.
476	23	1	وَالْعٰدِيٰتِ ضَبْحًاۙ	Demi kuda-kuda perang yang berlari kencang terengah-engah,
477	23	2	فَالْمُوْرِيٰتِ قَدْحًاۙ	yang memercikkan bunga api (dengan entakan kakinya),
478	23	3	فَالْمُغِيْرٰتِ صُبْحًاۙ	yang menyerang (dengan tiba-tiba) pada waktu pagi
479	23	4	فَاَثَرْنَ بِهٖ نَقْعًاۙ	sehingga menerbangkan debu,
480	23	5	فَوَسَطْنَ بِهٖ جَمْعًاۙ	lalu menyerbu ke tengah-tengah kumpulan musuh,
481	23	6	اِنَّ الْاِنْسَانَ لِرَبِّهٖ لَكَنُوْدٌ ۚ	sesungguhnya manusia itu sangatlah ingkar kepada Tuhannya.
482	23	7	وَاِنَّهٗ عَلٰى ذٰلِكَ لَشَهِيْدٌۚ	Sesungguhnya dia benar-benar menjadi saksi atas hal itu (keingkarannya).
483	23	8	وَاِنَّهٗ لِحُبِّ الْخَيْرِ لَشَدِيْدٌ ۗ	Sesungguhnya cintanya pada harta benar-benar berlebihan.
484	23	9	۞ اَفَلَا يَعْلَمُ اِذَا بُعْثِرَ مَا فِى الْقُبُوْرِۙ	Maka, tidakkah dia mengetahui (apa yang akan dialaminya) apabila dikeluarkan apa yang ada di dalam kubur
485	23	10	وَحُصِّلَ مَا فِى الصُّدُوْرِۙ	dan ditampakkan apa yang tersimpan di dalam dada?
486	23	11	اِنَّ رَبَّهُمْ بِهِمْ يَوْمَىِٕذٍ لَّخَبِيْرٌ ࣖ	Sesungguhnya Tuhan mereka pada hari itu benar-benar Maha Teliti terhadap (keadaan) mereka.
487	24	1	اَلْقَارِعَةُۙ	Al-Qāri‘ah (hari Kiamat yang menggetarkan).
488	24	2	مَا الْقَارِعَةُ ۚ	Apakah al-Qāri‘ah itu?
489	24	3	وَمَآ اَدْرٰىكَ مَا الْقَارِعَةُ ۗ	Tahukah kamu apakah al-Qāri‘ah itu?
490	24	4	يَوْمَ يَكُوْنُ النَّاسُ كَالْفَرَاشِ الْمَبْثُوْثِۙ	Pada hari itu manusia seperti laron yang beterbangan
491	24	5	وَتَكُوْنُ الْجِبَالُ كَالْعِهْنِ الْمَنْفُوْشِۗ	dan gunung-gunung seperti bulu yang berhamburan.
492	24	6	فَاَمَّا مَنْ ثَقُلَتْ مَوَازِيْنُهٗۙ	Siapa yang berat timbangan (kebaikan)-nya,
493	24	7	فَهُوَ فِيْ عِيْشَةٍ رَّاضِيَةٍۗ	dia berada dalam kehidupan yang menyenangkan.
494	24	8	وَاَمَّا مَنْ خَفَّتْ مَوَازِيْنُهٗۙ	Adapun orang yang ringan timbangan (kebaikan)-nya,
495	24	9	فَاُمُّهٗ هَاوِيَةٌ ۗ	tempat kembalinya adalah (neraka) Hawiyah.
496	24	10	وَمَآ اَدْرٰىكَ مَا هِيَهْۗ	Tahukah kamu apakah (neraka Hawiyah) itu?
497	24	11	نَارٌ حَامِيَةٌ ࣖ	(Ia adalah) api yang sangat panas.
498	25	1	اَلْهٰىكُمُ التَّكَاثُرُۙ	Berbangga-bangga dalam memperbanyak (dunia) telah melalaikanmu
499	25	2	حَتّٰى زُرْتُمُ الْمَقَابِرَۗ	sampai kamu masuk ke dalam kubur.
500	25	3	كَلَّا سَوْفَ تَعْلَمُوْنَۙ	Sekali-kali tidak! Kelak kamu akan mengetahui (akibat perbuatanmu itu).
501	25	4	ثُمَّ كَلَّا سَوْفَ تَعْلَمُوْنَ	Sekali-kali tidak (jangan melakukan itu)! Kelak kamu akan mengetahui (akibatnya).
502	25	5	كَلَّا لَوْ تَعْلَمُوْنَ عِلْمَ الْيَقِيْنِۗ	Sekali-kali tidak (jangan melakukan itu)! Sekiranya kamu mengetahui dengan pasti, (niscaya kamu tidak akan melakukannya).
503	25	6	لَتَرَوُنَّ الْجَحِيْمَۙ	Pasti kamu benar-benar akan melihat (neraka) Jahim.
504	25	7	ثُمَّ لَتَرَوُنَّهَا عَيْنَ الْيَقِيْنِۙ	Kemudian, kamu pasti benar-benar akan melihatnya dengan ainulyakin.
505	25	8	ثُمَّ لَتُسْـَٔلُنَّ يَوْمَىِٕذٍ عَنِ النَّعِيْمِ ࣖ	Kemudian, kamu pasti benar-benar akan ditanya pada hari itu tentang kenikmatan (yang megah di dunia itu).
506	26	1	وَالْعَصْرِۙ	Demi masa,
507	26	2	اِنَّ الْاِنْسَانَ لَفِيْ خُسْرٍۙ	sesungguhnya manusia benar-benar berada dalam kerugian,
508	26	3	اِلَّا الَّذِيْنَ اٰمَنُوْا وَعَمِلُوا الصّٰلِحٰتِ وَتَوَاصَوْا بِالْحَقِّ ەۙ وَتَوَاصَوْا بِالصَّبْرِ ࣖ	kecuali orang-orang yang beriman dan beramal saleh serta saling menasihati untuk kebenaran dan kesabaran.
509	27	1	وَيْلٌ لِّكُلِّ هُمَزَةٍ لُّمَزَةٍۙ	Celakalah setiap pengumpat lagi pencela
510	27	2	الَّذِيْ جَمَعَ مَالًا وَّعَدَّدَهٗۙ	yang mengumpulkan harta dan menghitung-hitungnya.
511	27	3	يَحْسَبُ اَنَّ مَالَهٗٓ اَخْلَدَهٗۚ	Dia (manusia) mengira bahwa hartanya dapat mengekalkannya.
512	27	4	كَلَّا لَيُنْۢبَذَنَّ فِى الْحُطَمَةِۖ	Sekali-kali tidak! Pasti dia akan dilemparkan ke dalam (neraka) Hutamah.
513	27	5	وَمَآ اَدْرٰىكَ مَا الْحُطَمَةُ ۗ	Tahukah kamu apakah (neraka) Hutamah?
514	27	6	نَارُ اللّٰهِ الْمُوْقَدَةُۙ	(Ia adalah) api (azab) Allah yang dinyalakan
515	27	7	الَّتِيْ تَطَّلِعُ عَلَى الْاَفْـِٕدَةِۗ	yang (membakar) naik sampai ke hati.
516	27	8	اِنَّهَا عَلَيْهِمْ مُّؤْصَدَةٌۙ	Sesungguhnya dia (api itu) tertutup rapat (sebagai hukuman) atas mereka,
517	27	9	فِيْ عَمَدٍ مُّمَدَّدَةٍ ࣖ	(sedangkan mereka) diikat pada tiang-tiang yang panjang.
518	28	1	اَلَمْ تَرَ كَيْفَ فَعَلَ رَبُّكَ بِاَصْحٰبِ الْفِيْلِۗ	Tidakkah engkau (Nabi Muhammad) memperhatikan bagaimana Tuhanmu telah bertindak terhadap pasukan bergajah?
519	28	2	اَلَمْ يَجْعَلْ كَيْدَهُمْ فِيْ تَضْلِيْلٍۙ	Bukankah Dia telah menjadikan tipu daya mereka itu sia-sia?
520	28	3	وَّاَرْسَلَ عَلَيْهِمْ طَيْرًا اَبَابِيْلَۙ	Dia mengirimkan kepada mereka burung yang berbondong-bondong.
521	28	4	تَرْمِيْهِمْ بِحِجَارَةٍ مِّنْ سِجِّيْلٍۙ	yang melempari mereka dengan batu dari tanah liat yang dibakar,
522	28	5	فَجَعَلَهُمْ كَعَصْفٍ مَّأْكُوْلٍ ࣖ	sehingga Dia menjadikan mereka seperti daun-daun yang dimakan (ulat).
523	29	1	لِاِيْلٰفِ قُرَيْشٍۙ	Disebabkan oleh kebiasaan orang-orang Quraisy,
524	29	2	اٖلٰفِهِمْ رِحْلَةَ الشِّتَاۤءِ وَالصَّيْفِۚ	(yaitu) kebiasaan mereka bepergian pada musim dingin dan musim panas (sehingga mendapatkan banyak keuntungan),
525	29	3	فَلْيَعْبُدُوْا رَبَّ هٰذَا الْبَيْتِۙ	maka hendaklah mereka menyembah Tuhan (pemilik) rumah ini (Ka‘bah)
526	29	4	الَّذِيْٓ اَطْعَمَهُمْ مِّنْ جُوْعٍ ەۙ وَّاٰمَنَهُمْ مِّنْ خَوْفٍ ࣖ	yang telah memberi mereka makanan untuk menghilangkan lapar dan mengamankan mereka dari rasa takut.
527	30	1	اَرَءَيْتَ الَّذِيْ يُكَذِّبُ بِالدِّيْنِۗ	Tahukah kamu (orang) yang mendustakan agama?
528	30	2	فَذٰلِكَ الَّذِيْ يَدُعُّ الْيَتِيْمَۙ	Itulah orang yang menghardik anak yatim
529	30	3	وَلَا يَحُضُّ عَلٰى طَعَامِ الْمِسْكِيْنِۗ	dan tidak menganjurkan untuk memberi makan orang miskin.
530	30	4	فَوَيْلٌ لِّلْمُصَلِّيْنَۙ	Celakalah orang-orang yang melaksanakan salat,
531	30	5	الَّذِيْنَ هُمْ عَنْ صَلَاتِهِمْ سَاهُوْنَۙ	(yaitu) yang lalai terhadap salatnya,
532	30	6	الَّذِيْنَ هُمْ يُرَاۤءُوْنَۙ	yang berbuat riya,
533	30	7	وَيَمْنَعُوْنَ الْمَاعُوْنَ ࣖ	dan enggan (memberi) bantuan.
534	31	1	اِنَّآ اَعْطَيْنٰكَ الْكَوْثَرَۗ	Sesungguhnya Kami telah memberimu (Nabi Muhammad) nikmat yang banyak.
535	31	2	فَصَلِّ لِرَبِّكَ وَانْحَرْۗ	Maka, laksanakanlah salat karena Tuhanmu dan berkurbanlah!
536	31	3	اِنَّ شَانِئَكَ هُوَ الْاَبْتَرُ ࣖ	Sesungguhnya orang yang membencimu, dialah yang terputus (dari rahmat Allah).
537	32	1	قُلْ يٰٓاَيُّهَا الْكٰفِرُوْنَۙ	Katakanlah (Nabi Muhammad), “Wahai orang-orang kafir,
538	32	2	لَآ اَعْبُدُ مَا تَعْبُدُوْنَۙ	aku tidak akan menyembah apa yang kamu sembah.
539	32	3	وَلَآ اَنْتُمْ عٰبِدُوْنَ مَآ اَعْبُدُۚ	Kamu juga bukan penyembah apa yang aku sembah.
540	32	4	وَلَآ اَنَا۠ عَابِدٌ مَّا عَبَدْتُّمْۙ	Aku juga tidak pernah menjadi penyembah apa yang kamu sembah.
541	32	5	وَلَآ اَنْتُمْ عٰبِدُوْنَ مَآ اَعْبُدُۗ	Kamu tidak pernah (pula) menjadi penyembah apa yang aku sembah.
542	32	6	لَكُمْ دِيْنُكُمْ وَلِيَ دِيْنِ ࣖ	Untukmu agamamu dan untukku agamaku.”
543	33	1	اِذَا جَاۤءَ نَصْرُ اللّٰهِ وَالْفَتْحُۙ	Apabila telah datang pertolongan Allah dan kemenangan
544	33	2	وَرَاَيْتَ النَّاسَ يَدْخُلُوْنَ فِيْ دِيْنِ اللّٰهِ اَفْوَاجًاۙ	dan engkau melihat manusia berbondong-bondong masuk agama Allah,
545	33	3	فَسَبِّحْ بِحَمْدِ رَبِّكَ وَاسْتَغْفِرْهُۗ اِنَّهٗ كَانَ تَوَّابًا ࣖ	bertasbihlah dengan memuji Tuhanmu dan mohonlah ampun kepada-Nya. Sesungguhnya Dia Maha Penerima tobat.
546	34	1	تَبَّتْ يَدَآ اَبِيْ لَهَبٍ وَّتَبَّۗ	Binasalah kedua tangan Abu Lahab dan benar-benar binasa dia.
547	34	2	مَآ اَغْنٰى عَنْهُ مَالُهٗ وَمَا كَسَبَۗ	Tidaklah berguna baginya hartanya dan apa yang dia usahakan.
548	34	3	سَيَصْلٰى نَارًا ذَاتَ لَهَبٍۙ	Kelak dia akan memasuki api yang bergejolak (neraka),
549	34	4	وَّامْرَاَتُهٗ ۗحَمَّالَةَ الْحَطَبِۚ	(begitu pula) istrinya, pembawa kayu bakar (penyebar fitnah).
550	34	5	فِيْ جِيْدِهَا حَبْلٌ مِّنْ مَّسَدٍ ࣖ	Di lehernya ada tali dari sabut yang dipintal.
551	35	1	قُلْ هُوَ اللّٰهُ اَحَدٌۚ	Katakanlah (Nabi Muhammad), “Dialah Allah Yang Maha Esa.
552	35	2	اَللّٰهُ الصَّمَدُۚ	Allah tempat meminta segala sesuatu.
553	35	3	لَمْ يَلِدْ وَلَمْ يُوْلَدْۙ	Dia tidak beranak dan tidak pula diperanakkan
554	35	4	وَلَمْ يَكُنْ لَّهٗ كُفُوًا اَحَدٌ ࣖ	serta tidak ada sesuatu pun yang setara dengan-Nya.”
555	36	1	قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ	Katakanlah (Nabi Muhammad), “Aku berlindung kepada Tuhan yang (menjaga) fajar (subuh)
556	36	2	مِنْ شَرِّ مَا خَلَقَۙ	dari kejahatan (makhluk yang) Dia ciptakan,
557	36	3	وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ	dari kejahatan malam apabila telah gelap gulita,
558	36	4	وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ	dari kejahatan perempuan-perempuan (penyihir) yang meniup pada buhul-buhul (talinya),
559	36	5	وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَ ࣖ	dan dari kejahatan orang yang dengki apabila dia dengki.”
560	37	1	قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ	Katakanlah (Nabi Muhammad), “Aku berlindung kepada Tuhan manusia,
561	37	2	مَلِكِ النَّاسِۙ	raja manusia,
562	37	3	اِلٰهِ النَّاسِۙ	sembahan manusia
563	37	4	مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ	dari kejahatan (setan) pembisik yang bersembunyi
564	37	5	الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ	yang membisikkan (kejahatan) ke dalam dada manusia,
565	37	6	مِنَ الْجِنَّةِ وَالنَّاسِ ࣖ	dari (golongan) jin dan manusia.”
566	37	1	قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ	Katakanlah (Nabi Muhammad), “Aku berlindung kepada Tuhan manusia,
567	37	2	مَلِكِ النَّاسِۙ	raja manusia,
568	37	3	اِلٰهِ النَّاسِۙ	sembahan manusia
569	37	4	مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ	dari kejahatan (setan) pembisik yang bersembunyi
570	37	5	الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ	yang membisikkan (kejahatan) ke dalam dada manusia,
571	37	6	مِنَ الْجِنَّةِ وَالنَّاسِ ࣖ	dari (golongan) jin dan manusia.”
\.


--
-- Data for Name: detail_sholat_wajib; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detail_sholat_wajib (id, id_ibadah_harian, id_kategori_sholat_wajib, id_status_sholat_wajib) FROM stdin;
1	1	1	1
2	1	2	1
3	1	3	2
4	1	4	1
5	1	5	1
6	8	1	1
7	8	2	1
8	8	3	1
9	8	4	1
10	8	5	1
20	19	1	1
21	19	2	1
22	19	3	1
23	19	4	1
24	19	5	1
25	20	1	1
26	20	2	1
27	20	3	1
28	20	4	1
29	20	5	1
30	21	1	1
31	21	2	1
32	21	3	1
33	21	4	1
34	21	5	1
35	22	1	1
36	22	2	1
37	22	3	1
38	22	4	1
39	22	5	1
40	23	1	2
41	23	2	3
42	23	3	1
43	23	4	1
44	23	5	1
45	24	1	1
46	24	2	1
47	24	3	1
48	24	4	1
49	24	5	1
52	26	1	1
53	26	2	1
54	26	3	1
55	26	4	1
56	26	5	1
57	27	1	1
58	27	2	1
59	27	3	1
60	27	4	1
61	27	5	1
62	28	1	1
63	28	2	1
64	28	3	1
65	28	4	1
66	28	5	1
\.


--
-- Sequence values — from ramadhan_schema.sql (only for sequences with loaded data)
--

--
-- Name: hukum_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hukum_id_seq', 1, false);


--
-- Name: role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_id_seq', 3, true);


--
-- Name: status_absensi_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.status_absensi_id_seq', 4, true);


--
-- Name: status_setoran_hafalan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.status_setoran_hafalan_id_seq', 2, true);


--
-- Name: status_sholat_wajib_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.status_sholat_wajib_id_seq', 3, true);


--
-- Name: kategori_sholat_wajib_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.kategori_sholat_wajib_id_seq', 5, true);


--
-- Name: kategori_sunnah_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.kategori_sunnah_id_seq', 5, true);


--
-- Name: bacaan_sholat_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bacaan_sholat_id_seq', 19, true);


--
-- Name: dzikir_setelah_sholat_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.dzikir_setelah_sholat_id_seq', 20, true);


--
-- Name: ayat_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ayat_id_seq', 571, true);


--
-- Name: detail_sholat_wajib_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.detail_sholat_wajib_id_seq', 66, true);


--
-- Constraints (primary keys, unique, foreign keys) — from setup.sql
--

-- Name: absensi absensi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.absensi
    ADD CONSTRAINT absensi_pkey PRIMARY KEY (id);


--
-- Name: ayat ayat_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ayat
    ADD CONSTRAINT ayat_pkey PRIMARY KEY (id);


--
-- Name: bacaan_sholat bacaan_sholat_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bacaan_sholat
    ADD CONSTRAINT bacaan_sholat_pkey PRIMARY KEY (id);


--
-- Name: detail_sholat_wajib detail_sholat_wajib_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib
    ADD CONSTRAINT detail_sholat_wajib_pkey PRIMARY KEY (id);


--
-- Name: dzikir_setelah_sholat dzikir_setelah_sholat_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.dzikir_setelah_sholat
    ADD CONSTRAINT dzikir_setelah_sholat_pkey PRIMARY KEY (id);


--
-- Name: hukum hukum_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hukum
    ADD CONSTRAINT hukum_pkey PRIMARY KEY (id);


--
-- Name: ibadah_harian ibadah_harian_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_harian
    ADD CONSTRAINT ibadah_harian_pkey PRIMARY KEY (id);


--
-- Name: ibadah_sunnah ibadah_sunnah_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_sunnah
    ADD CONSTRAINT ibadah_sunnah_pkey PRIMARY KEY (id);


--
-- Name: kategori_sholat_wajib kategori_sholat_wajib_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kategori_sholat_wajib
    ADD CONSTRAINT kategori_sholat_wajib_pkey PRIMARY KEY (id);


--
-- Name: kategori_sunnah kategori_sunnah_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kategori_sunnah
    ADD CONSTRAINT kategori_sunnah_pkey PRIMARY KEY (id);


--
-- Name: kegiatan kegiatan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan
    ADD CONSTRAINT kegiatan_pkey PRIMARY KEY (id);


--
-- Name: kegiatan_user kegiatan_user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan_user
    ADD CONSTRAINT kegiatan_user_pkey PRIMARY KEY (id);


--
-- Name: kelas kelas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kelas
    ADD CONSTRAINT kelas_pkey PRIMARY KEY (id);


--
-- Name: role role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (id);


--
-- Name: setoran_hafalan setoran_hafalan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT setoran_hafalan_pkey PRIMARY KEY (id);


--
-- Name: status_absensi status_absensi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_absensi
    ADD CONSTRAINT status_absensi_pkey PRIMARY KEY (id);


--
-- Name: status_setoran_hafalan status_setoran_hafalan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_setoran_hafalan
    ADD CONSTRAINT status_setoran_hafalan_pkey PRIMARY KEY (id);


--
-- Name: status_sholat_wajib status_sholat_wajib_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.status_sholat_wajib
    ADD CONSTRAINT status_sholat_wajib_pkey PRIMARY KEY (id);


--
-- Name: surah surah_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.surah
    ADD CONSTRAINT surah_pkey PRIMARY KEY (id);


--
-- Name: tausiah tausiah_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tausiah
    ADD CONSTRAINT tausiah_pkey PRIMARY KEY (id);


--
-- Name: detail_sholat_wajib unique_ibadah_kategori; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib
    ADD CONSTRAINT unique_ibadah_kategori UNIQUE (id_ibadah_harian, id_kategori_sholat_wajib);


--
-- Name: ibadah_harian unique_user_tanggal; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_harian
    ADD CONSTRAINT unique_user_tanggal UNIQUE (id_user, tanggal);


--
-- Name: kegiatan_user uq_kegiatan_user; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan_user
    ADD CONSTRAINT uq_kegiatan_user UNIQUE (id_user, id_kegiatan);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- Name: absensi fk_absensi_status; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.absensi
    ADD CONSTRAINT fk_absensi_status FOREIGN KEY (id_status_absensi) REFERENCES public.status_absensi(id);


--
-- Name: absensi fk_absensi_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.absensi
    ADD CONSTRAINT fk_absensi_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: ayat fk_ayat_surah; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ayat
    ADD CONSTRAINT fk_ayat_surah FOREIGN KEY (id_surah) REFERENCES public.surah(id);


--
-- Name: bacaan_sholat fk_bacaan_hukum; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bacaan_sholat
    ADD CONSTRAINT fk_bacaan_hukum FOREIGN KEY (id_hukum) REFERENCES public.hukum(id);


--
-- Name: detail_sholat_wajib fk_detail_ibadah; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib
    ADD CONSTRAINT fk_detail_ibadah FOREIGN KEY (id_ibadah_harian) REFERENCES public.ibadah_harian(id);


--
-- Name: detail_sholat_wajib fk_detail_kategori; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib
    ADD CONSTRAINT fk_detail_kategori FOREIGN KEY (id_kategori_sholat_wajib) REFERENCES public.kategori_sholat_wajib(id);


--
-- Name: detail_sholat_wajib fk_detail_status; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.detail_sholat_wajib
    ADD CONSTRAINT fk_detail_status FOREIGN KEY (id_status_sholat_wajib) REFERENCES public.status_sholat_wajib(id);


--
-- Name: ibadah_harian fk_ibadah_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_harian
    ADD CONSTRAINT fk_ibadah_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: kegiatan_user fk_kegiatan; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan_user
    ADD CONSTRAINT fk_kegiatan FOREIGN KEY (id_kegiatan) REFERENCES public.kegiatan(id);


--
-- Name: kegiatan_user fk_kegiatan_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kegiatan_user
    ADD CONSTRAINT fk_kegiatan_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: setoran_hafalan fk_setoran_bacaan; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT fk_setoran_bacaan FOREIGN KEY (id_bacaan_sholat) REFERENCES public.bacaan_sholat(id);


--
-- Name: setoran_hafalan fk_setoran_kelas; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT fk_setoran_kelas FOREIGN KEY (id_kelas) REFERENCES public.kelas(id);


--
-- Name: setoran_hafalan fk_setoran_status; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT fk_setoran_status FOREIGN KEY (id_status_setoran_hafalan) REFERENCES public.status_setoran_hafalan(id);


--
-- Name: setoran_hafalan fk_setoran_surah; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT fk_setoran_surah FOREIGN KEY (id_surah) REFERENCES public.surah(id);


--
-- Name: setoran_hafalan fk_setoran_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.setoran_hafalan
    ADD CONSTRAINT fk_setoran_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: ibadah_sunnah fk_sunnah_kategori; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_sunnah
    ADD CONSTRAINT fk_sunnah_kategori FOREIGN KEY (id_kategori_sunnah) REFERENCES public.kategori_sunnah(id);


--
-- Name: ibadah_sunnah fk_sunnah_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ibadah_sunnah
    ADD CONSTRAINT fk_sunnah_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: tausiah fk_tausiah_user; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tausiah
    ADD CONSTRAINT fk_tausiah_user FOREIGN KEY (id_user) REFERENCES public.users(id);


--
-- Name: users fk_user_kelas; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT fk_user_kelas FOREIGN KEY (id_kelas) REFERENCES public.kelas(id);


--
-- Name: users fk_user_role; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT fk_user_role FOREIGN KEY (id_role) REFERENCES public.role(id);




--
-- MERGED dump complete
--
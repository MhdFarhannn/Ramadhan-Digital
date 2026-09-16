--
-- PostgreSQL database dump
--

\restrict YdjmdkZTXFgBCqPso7ffqNCE24C9bXPsf1GMeMxi3qvEIxxRapiPMWUNlparR6A

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
-- PostgreSQL database dump complete
--

\unrestrict YdjmdkZTXFgBCqPso7ffqNCE24C9bXPsf1GMeMxi3qvEIxxRapiPMWUNlparR6A


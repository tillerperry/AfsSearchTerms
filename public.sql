/*
 Navicat Premium Data Transfer

 Source Server         : HUBTLDBDB
 Source Server Type    : PostgreSQL
 Source Server Version : 150001 (150001)
 Source Host           : localhost:5432
 Source Catalog        : AfsSearch
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 150001 (150001)
 File Encoding         : 65001

 Date: 12/03/2024 22:40:01
*/


-- ----------------------------
-- Table structure for TranslationSearch
-- ----------------------------
DROP TABLE IF EXISTS "public"."TranslationSearch";
CREATE TABLE "public"."TranslationSearch" (
  "Id" text COLLATE "pg_catalog"."default" NOT NULL,
  "Translated" text COLLATE "pg_catalog"."default",
  "Translation" text COLLATE "pg_catalog"."default",
  "Text" text COLLATE "pg_catalog"."default",
  "CreatedAt" timestamp(6)
)
;
ALTER TABLE "public"."TranslationSearch" OWNER TO "postgres";

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS "public"."__EFMigrationsHistory";
CREATE TABLE "public"."__EFMigrationsHistory" (
  "MigrationId" varchar(150) COLLATE "pg_catalog"."default" NOT NULL,
  "ProductVersion" varchar(32) COLLATE "pg_catalog"."default" NOT NULL
)
;
ALTER TABLE "public"."__EFMigrationsHistory" OWNER TO "postgres";

-- ----------------------------
-- Primary Key structure for table TranslationSearch
-- ----------------------------
ALTER TABLE "public"."TranslationSearch" ADD CONSTRAINT "TranslationSearch_pkey" PRIMARY KEY ("Id");

-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE "public"."__EFMigrationsHistory" ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");

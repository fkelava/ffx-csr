﻿using FFX_Cutscene_Remover.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace FFXCutsceneRemover
{
    class NewGameTransition : Transition
    {
        // Version Number, 0x30 - 0x39 = 0 - 9, 0x48 = decimal point
        private const byte majorID = 0x31;
        private const byte minorID = 0x32;
        private const byte patchID = 0x32;

        private byte[][] NewGameText = new byte[][]
        {
            // Japanese
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x6F, 0x00, 0x00, 0x00, 0x6F, 0x00, 0x00, 0x00, 0x81, 0x00, 0x00, 0x02, 0x81, 0x00, 0x00, 0x02, 0xCC, 0x00, 0x00, 0x02, 0xCC, 0x00, 0x00, 0x02, 0xF9, 0x00, 0x00, 0x02,
                            0xF9, 0x00, 0x00, 0x02, 0x2B, 0x01, 0x00, 0x00, 0x2B, 0x01, 0x00, 0x00, 0x3D, 0x01, 0x00, 0x02, 0x3D, 0x01, 0x00, 0x02, 0x63, 0x01, 0x00, 0x02, 0x63, 0x01, 0x00, 0x02, 0xBF, 0x01, 0x00, 0x02, 0xBF, 0x01, 0x00, 0x02,
                            0x1B, 0x02, 0x00, 0x02, 0x1B, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10,
                            0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0xC7, 0xE3, 0xB1, 0xB0, 0x2D, 0xD4, 0x8C, 0x2D, 0x68, 0x2D, 0x87, 0x4E, 0x00, 0x09, 0x30, 0xC7, 0xE3, 0xB1, 0xB0, 0x2D, 0xD4, 0xAD, 0x2D, 0x68, 0xAE, 0x85, 0x6D, 0x7E,
                            0x73, 0x62, 0x3B, 0x03, 0x42, 0xE5, 0xFA, 0xB2, 0x2D, 0x93, 0x8C, 0x2D, 0x5E, 0xA8, 0x2A, 0x30, 0x66, 0x8D, 0x85, 0x6B, 0x9C, 0x79, 0xAE, 0x43, 0x03, 0x07, 0x46, 0x10, 0x30, 0x2D, 0x9E, 0x2D, 0x46, 0xC7, 0xE3, 0xB1,
                            0xB0, 0x2D, 0xD4, 0x03, 0x07, 0x46, 0x10, 0x31, 0x2D, 0x43, 0x2A, 0x31, 0x2E, 0xCD, 0x2E, 0xF4, 0x6F, 0xC7, 0xE3, 0xB1, 0xB0, 0x2D, 0xD4, 0x00, 0x09, 0x30, 0x0A, 0x88, 0x4C, 0x2D, 0x9E, 0x2D, 0x46, 0xC7, 0xE3, 0xB1,
                            0xB0, 0x2D, 0xD4, 0x4E, 0x0A, 0x41, 0x03, 0x07, 0x31, 0xA6, 0xAB, 0x75, 0x62, 0x85, 0x77, 0x69, 0x4F, 0x03, 0x07, 0x44, 0x10, 0x30, 0x8D, 0x62, 0x03, 0x07, 0x44, 0x10, 0x31, 0x62, 0x62, 0x66, 0x00, 0x09, 0x30, 0x0A,
                            0x88, 0x4C, 0x2D, 0x43, 0x2A, 0x31, 0x2E, 0xCD, 0x2E, 0xF4, 0x6F, 0xC7, 0xE3, 0xB1, 0xB0, 0x2D, 0xD4, 0x4E, 0x0A, 0x41, 0x03, 0x07, 0x31, 0xA6, 0xAB, 0x75, 0x62, 0x85, 0x77, 0x69, 0x4F, 0x03, 0x07, 0x47, 0x10, 0x30,
                            0x8D, 0x62, 0x03, 0x07, 0x47, 0x10, 0x31, 0x62, 0x62, 0x66, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0x2C, 0x31, 0x2C, 0x36, 0x2C, 0x3C, 0x8C, 0x2D, 0x68, 0x2D, 0x87, 0x4E, 0x00, 0x09, 0x30, 0x2C, 0x31, 0x2C, 0x36, 0x2C,
                            0x3C, 0xAD, 0x2D, 0x68, 0xAE, 0x85, 0x6D, 0x7E, 0x73, 0x62, 0x3B, 0x03, 0x07, 0x45, 0x10, 0x30, 0xB0, 0xFA, 0xFE, 0xC6, 0x03, 0x07, 0x45, 0x10, 0x31, 0xB8, 0xF8, 0xC6, 0xD8, 0xF9, 0x00, 0x09, 0x30, 0x4C, 0x2C, 0x32,
                            0x2C, 0x5E, 0x2C, 0x5D, 0x2C, 0x5C, 0x2C, 0x4C, 0x2C, 0x4E, 0x2C, 0x57, 0x2C, 0x4E, 0x3A, 0x2C, 0x42, 0x2C, 0x54, 0x2C, 0x52, 0x2C, 0x59, 0x3A, 0x2C, 0x41, 0x2C, 0x4E, 0x2C, 0x56, 0x2C, 0x58, 0x2C, 0x5F, 0x2C, 0x4E,
                            0x2C, 0x5B, 0x3A, 0x2C, 0x5F,
                            majorID, 0x48, minorID, 0x48, patchID, 
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x2C, 0x42, 0x2C, 0x5D, 0x2C, 0x4A, 0x2C, 0x5B, 0x2C, 0x5D, 0x3A, 0x2C, 0x36, 0x2C, 0x4A, 0x2C, 0x56, 0x2C, 0x4E, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0x8D, 0x62, 0x03, 0x07, 0x50, 0x10,
                            0x31, 0x62, 0x62, 0x66, 0x00, 0x09, 0x30, 0x4C, 0x2C, 0x32, 0x2C, 0x5E, 0x2C, 0x5D, 0x2C, 0x5C, 0x2C, 0x4C, 0x2C, 0x4E, 0x2C, 0x57, 0x2C, 0x4E, 0x3A, 0x2C, 0x42, 0x2C, 0x54, 0x2C, 0x52, 0x2C, 0x59, 0x3A, 0x2C, 0x41,
                            0x2C, 0x4E, 0x2C, 0x56, 0x2C, 0x58, 0x2C, 0x5F, 0x2C, 0x4E, 0x2C, 0x5B, 0x3A, 0x2C, 0x5F,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x2C, 0x42, 0x2C, 0x5D, 0x2C, 0x4A, 0x2C, 0x5B, 0x2C, 0x5D, 0x3A, 0x2C, 0x36, 0x2C, 0x4A, 0x2C, 0x56, 0x2C, 0x4E, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0x8D, 0x62, 0x03, 0x07, 0x50, 0x10,
                            0x31, 0x62, 0x62, 0x66, 0x00, 0x09, 0x30, 0xDD, 0x3C, 0xD7, 0xD5, 0xB1, 0xC7, 0xBD, 0xD7, 0xF7, 0xB2, 0xE4, 0xAD, 0x2C, 0xE4, 0x2E, 0x3D, 0x75, 0x9C, 0x77, 0x69, 0x4F, 0x03, 0x10, 0x30, 0x8D, 0x62, 0x03, 0x10, 0x31,
                            0x62, 0x62, 0x66, 0x00 },
            // English
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x77, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0xA2, 0x00, 0x00, 0x02, 0xA2, 0x00, 0x00, 0x02, 0xF2, 0x00, 0x00, 0x02, 0xF2, 0x00, 0x00, 0x02, 0x77, 0x01, 0x00, 0x02,
                            0x77, 0x01, 0x00, 0x02, 0xFB, 0x01, 0x00, 0x00, 0xFB, 0x01, 0x00, 0x00, 0x0C, 0x02, 0x00, 0x02, 0x0C, 0x02, 0x00, 0x02, 0x42, 0x02, 0x00, 0x02, 0x42, 0x02, 0x00, 0x02, 0x81, 0x02, 0x00, 0x02, 0x81, 0x02, 0x00, 0x02,
                            0xC0, 0x02, 0x00, 0x02, 0xC0, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x52, 0x77, 0x70, 0x81, 0x70, 0x72, 0x83, 0x74, 0x81, 0x3A, 0x50, 0x73, 0x85, 0x70, 0x7D, 0x72, 0x74, 0x7C, 0x74, 0x7D, 0x83,
                            0x3A, 0x62, 0x88, 0x82, 0x83, 0x74, 0x7C, 0x3A, 0x62, 0x74, 0x7B, 0x74, 0x72, 0x83, 0x78, 0x7E, 0x7D, 0x00, 0x09, 0x30, 0x62, 0x74, 0x7B, 0x74, 0x72, 0x83, 0x3A, 0x70, 0x3A, 0x62, 0x7F, 0x77, 0x74, 0x81, 0x74, 0x3A,
                            0x56, 0x81, 0x78, 0x73, 0x3A, 0x83, 0x7E, 0x3A, 0x84, 0x82, 0x74, 0x3B, 0x03, 0x03, 0x07, 0x44, 0x10, 0x30, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x3A, 0x62, 0x7F, 0x77, 0x74, 0x81, 0x74, 0x3A, 0x56, 0x81,
                            0x78, 0x73, 0x03, 0x07, 0x44, 0x10, 0x31, 0x54, 0x87, 0x7F, 0x74, 0x81, 0x83, 0x3A, 0x62, 0x7F, 0x77, 0x74, 0x81, 0x74, 0x3A, 0x56, 0x81, 0x78, 0x73, 0x00, 0x09, 0x30, 0x07, 0x31, 0x68, 0x7E, 0x84, 0x3A, 0x77, 0x70,
                            0x85, 0x74, 0x3A, 0x82, 0x74, 0x7B, 0x74, 0x72, 0x83, 0x74, 0x73, 0x3A, 0x83, 0x77, 0x74, 0x4A, 0x03, 0x07, 0x31, 0x0A, 0x88, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x3A, 0x62, 0x7F, 0x77, 0x74, 0x81, 0x74,
                            0x3A, 0x56, 0x81, 0x78, 0x73, 0x0A, 0x41, 0x03, 0x42, 0x68, 0x7E, 0x84, 0x3A, 0x72, 0x70, 0x7D, 0x7D, 0x7E, 0x83, 0x3A, 0x82, 0x86, 0x78, 0x83, 0x72, 0x77, 0x3A, 0x56, 0x81, 0x78, 0x73, 0x82, 0x3A, 0x78, 0x7D, 0x47,
                            0x76, 0x70, 0x7C, 0x74, 0x3B, 0x43, 0x03, 0x07, 0x31, 0x58, 0x82, 0x3A, 0x83, 0x77, 0x78, 0x82, 0x3A, 0x83, 0x77, 0x74, 0x3A, 0x7E, 0x7D, 0x74, 0x3A, 0x88, 0x7E, 0x84, 0x3A, 0x86, 0x70, 0x7D, 0x83, 0x4F, 0x03, 0x03,
                            0x07, 0x51, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x07, 0x51, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x07, 0x31, 0x68, 0x7E, 0x84, 0x3A, 0x77, 0x70, 0x85, 0x74, 0x3A, 0x82, 0x74, 0x7B, 0x74, 0x72, 0x83, 0x74, 0x73,
                            0x3A, 0x83, 0x77, 0x74, 0x4A, 0x03, 0x07, 0x31, 0x0A, 0x88, 0x54, 0x87, 0x7F, 0x74, 0x81, 0x83, 0x3A, 0x62, 0x7F, 0x77, 0x74, 0x81, 0x74, 0x3A, 0x56, 0x81, 0x78, 0x73, 0x0A, 0x41, 0x48, 0x03, 0x42, 0x68, 0x7E, 0x84,
                            0x3A, 0x72, 0x70, 0x7D, 0x7D, 0x7E, 0x83, 0x3A, 0x82, 0x86, 0x78, 0x83, 0x72, 0x77, 0x3A, 0x56, 0x81, 0x78, 0x73, 0x82, 0x3A, 0x78, 0x7D, 0x47, 0x76, 0x70, 0x7C, 0x74, 0x3B, 0x43, 0x03, 0x07, 0x31, 0x58, 0x82, 0x3A,
                            0x83, 0x77, 0x78, 0x82, 0x3A, 0x83, 0x77, 0x74, 0x3A, 0x7E, 0x7D, 0x74, 0x3A, 0x88, 0x7E, 0x84, 0x3A, 0x86, 0x70, 0x7D, 0x83, 0x4F, 0x03, 0x03, 0x07, 0x51, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x07, 0x51, 0x10, 0x31,
                            0x5D, 0x7E, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x8F, 0x62, 0x7E, 0x84, 0x7D, 0x73, 0x83, 0x81, 0x70, 0x72, 0x7A, 0x90, 0x00, 0x09, 0x30, 0x62, 0x74, 0x7B, 0x74, 0x72, 0x83, 0x3A, 0x70, 0x3A, 0x82, 0x7E, 0x84, 0x7D, 0x73,
                            0x83, 0x81, 0x70, 0x72, 0x7A, 0x3A, 0x83, 0x88, 0x7F, 0x74, 0x48, 0x03, 0x07, 0x4A, 0x10, 0x30, 0x50, 0x81, 0x81, 0x70, 0x7D, 0x76, 0x74, 0x73, 0x03, 0x07, 0x4A, 0x10, 0x31, 0x5E, 0x81, 0x78, 0x76, 0x78, 0x7D, 0x70,
                            0x7B, 0x00, 0x09, 0x30, 0x6A, 0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x6A, 0x52,
                            0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x09, 0x30,
                            0x64, 0x82, 0x74, 0x3A, 0x83, 0x77, 0x74, 0x3A, 0x57, 0x53, 0x53, 0x4F, 0x03, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x10, 0x31, 0x5D, 0x7E, 0x00 },
            // French
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x83, 0x00, 0x00, 0x00, 0x83, 0x00, 0x00, 0x00, 0x99, 0x00, 0x00, 0x02, 0x99, 0x00, 0x00, 0x02, 0x2A, 0x01, 0x00, 0x02, 0x2A, 0x01, 0x00, 0x02, 0x7F, 0x01, 0x00, 0x02,
                            0x7F, 0x01, 0x00, 0x02, 0xD2, 0x01, 0x00, 0x00, 0xD2, 0x01, 0x00, 0x00, 0xE9, 0x01, 0x00, 0x02, 0xE9, 0x01, 0x00, 0x02, 0x29, 0x02, 0x00, 0x02, 0x29, 0x02, 0x00, 0x02, 0x69, 0x02, 0x00, 0x02, 0x69, 0x02, 0x00, 0x02,
                            0xA9, 0x02, 0x00, 0x02, 0xA9, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x07, 0xE8, 0x10, 0xFF, 0x03, 0x10, 0x31,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x07, 0xE8, 0x10, 0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x52, 0x57, 0x5E, 0x58, 0x67, 0x3A, 0x53, 0x64, 0x3A,
                            0x62, 0x5F, 0x57, 0x54, 0x61, 0x58, 0x54, 0x61, 0x00, 0x09, 0x30, 0x60, 0x84, 0x74, 0x7B, 0x3A, 0x83, 0x88, 0x7F, 0x74, 0x3A, 0x73, 0x74, 0x3A, 0x62, 0x7F, 0x77, 0xC0, 0x81, 0x78, 0x74, 0x81, 0x3A, 0x73, 0xC0, 0x82,
                            0x78, 0x81, 0x74, 0x89, 0x47, 0x85, 0x7E, 0x84, 0x82, 0x3A, 0x84, 0x83, 0x78, 0x7B, 0x78, 0x82, 0x74, 0x81, 0x3A, 0x4F, 0x03, 0x42, 0x58, 0x7B, 0x3A, 0x7D, 0x74, 0x3A, 0x7F, 0x7E, 0x84, 0x81, 0x81, 0x70, 0x3A, 0x7F,
                            0x70, 0x82, 0x3A, 0xC1, 0x83, 0x81, 0x74, 0x3A, 0x72, 0x77, 0x70, 0x7D, 0x76, 0xC0, 0x3A, 0x74, 0x7D, 0x3A, 0x72, 0x7E, 0x84, 0x81, 0x82, 0x3A, 0x73, 0x74, 0x3A, 0x7F, 0x70, 0x81, 0x83, 0x78, 0x74, 0x43, 0x03, 0x03,
                            0x07, 0x51, 0x07, 0xE2, 0x10, 0x30, 0x62, 0x7F, 0x77, 0xC0, 0x81, 0x78, 0x74, 0x81, 0x3A, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x03, 0x07, 0x51, 0x07, 0xE2, 0x10, 0x31, 0x62, 0x7F, 0x77, 0xC0, 0x81, 0x78,
                            0x74, 0x81, 0x3A, 0x54, 0x87, 0x7F, 0x74, 0x81, 0x83, 0x00, 0x09, 0x30, 0x54, 0x83, 0x74, 0x82, 0x47, 0x85, 0x7E, 0x84, 0x82, 0x3A, 0x82, 0xCE, 0x81, 0x42, 0x74, 0x43, 0x3A, 0x73, 0x74, 0x3A, 0x85, 0x7E, 0x84, 0x7B,
                            0x7E, 0x78, 0x81, 0x3A, 0x79, 0x7E, 0x84, 0x74, 0x81, 0x03, 0x70, 0x85, 0x74, 0x72, 0x3A, 0x7B, 0x74, 0x3A, 0x0A, 0x88, 0x62, 0x7F, 0x77, 0xC0, 0x81, 0x78, 0x74, 0x81, 0x3A, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81,
                            0x73, 0x0A, 0x41, 0x3A, 0x4F, 0x03, 0x03, 0x07, 0x50, 0x10, 0x30, 0x5E, 0x84, 0x78, 0x03, 0x07, 0x50, 0x10, 0x31, 0x5D, 0x7E, 0x7D, 0x00, 0x09, 0x30, 0x54, 0x83, 0x74, 0x82, 0x47, 0x85, 0x7E, 0x84, 0x82, 0x3A, 0x82,
                            0xCE, 0x81, 0x42, 0x74, 0x43, 0x3A, 0x73, 0x74, 0x3A, 0x85, 0x7E, 0x84, 0x7B, 0x7E, 0x78, 0x81, 0x3A, 0x79, 0x7E, 0x84, 0x74, 0x81, 0x03, 0x70, 0x85, 0x74, 0x72, 0x3A, 0x7B, 0x74, 0x3A, 0x0A, 0x88, 0x62, 0x7F, 0x77,
                            0xC0, 0x81, 0x78, 0x74, 0x81, 0x3A, 0x54, 0x87, 0x7F, 0x74, 0x81, 0x83, 0x0A, 0x41, 0x3A, 0x4F, 0x03, 0x03, 0x07, 0x50, 0x10, 0x30, 0x5E, 0x84, 0x78, 0x03, 0x07, 0x50, 0x10, 0x31, 0x5D, 0x7E, 0x7D, 0x00, 0x09, 0x30,
                            0x0A, 0x43, 0x52, 0x77, 0x7E, 0x78, 0x87, 0x3A, 0x73, 0x74, 0x82, 0x3A, 0x7C, 0x84, 0x82, 0x78, 0x80, 0x84, 0x74, 0x82, 0x00, 0x09, 0x30, 0x52, 0x77, 0x7E, 0x78, 0x82, 0x78, 0x82, 0x82, 0x74, 0x89, 0x3A, 0x7B, 0x74,
                            0x3A, 0x83, 0x88, 0x7F, 0x74, 0x3A, 0x73, 0x74, 0x3A, 0x7C, 0x84, 0x82, 0x78, 0x80, 0x84, 0x74, 0x82, 0x3A, 0x4A, 0x03, 0x07, 0x4B, 0x10, 0x30, 0x50, 0x81, 0x81, 0x70, 0x7D, 0x76, 0xC0, 0x74, 0x82, 0x03, 0x07, 0x4B,
                            0x10, 0x31, 0x5E, 0x81, 0x78, 0x76, 0x78, 0x7D, 0x70, 0x7B, 0x74, 0x82, 0x00, 0x09, 0x30, 0x6A, 0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74,
                            0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x5E, 0x84, 0x78, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x7D, 0x00, 0x09, 0x30, 0x6A,
                            0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x5E, 0x84, 0x78, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x7D, 0x00, 0x09, 0x30, 0x09,
                            0x30, 0x64, 0x82, 0x74, 0x3A, 0x77, 0x70, 0x81, 0x73, 0x3A, 0x73, 0x78, 0x82, 0x7A, 0x3A, 0x73, 0x81, 0x78, 0x85, 0x74, 0x4F, 0x03, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x10, 0x31, 0x5D, 0x7E, 0x00 },
            // Spanish
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x77, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x9F, 0x00, 0x00, 0x02, 0x9F, 0x00, 0x00, 0x02, 0x05, 0x01, 0x00, 0x02, 0x05, 0x01, 0x00, 0x02, 0x7F, 0x01, 0x00, 0x02,
                            0x7F, 0x01, 0x00, 0x02, 0xFB, 0x01, 0x00, 0x00, 0xFB, 0x01, 0x00, 0x00, 0x06, 0x02, 0x00, 0x02, 0x06, 0x02, 0x00, 0x02, 0x40, 0x02, 0x00, 0x02, 0x40, 0x02, 0x00, 0x02, 0x80, 0x02, 0x00, 0x02, 0x80, 0x02, 0x00, 0x02,
                            0xC0, 0x02, 0x00, 0x02, 0xC0, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x62, 0x74, 0x7B, 0x74, 0x72, 0x72, 0x78, 0xC9, 0x7D, 0x3A, 0x73, 0x74, 0x7B, 0x3A, 0x82, 0x78, 0x82, 0x83, 0x74, 0x7C, 0x70,
                            0x3A, 0x73, 0x74, 0x3A, 0x73, 0x74, 0x82, 0x70, 0x81, 0x81, 0x7E, 0x7B, 0x7B, 0x7E, 0x00, 0x09, 0x30, 0x54, 0x7B, 0x78, 0x76, 0x74, 0x3A, 0x74, 0x7B, 0x3A, 0x83, 0x70, 0x71, 0x7B, 0x74, 0x81, 0x7E, 0x3A, 0x80, 0x84,
                            0x74, 0x3A, 0x80, 0x84, 0x78, 0x74, 0x81, 0x74, 0x82, 0x3A, 0x84, 0x82, 0x70, 0x81, 0x4A, 0x03, 0x03, 0x07, 0x42, 0x10, 0x30, 0x63, 0x70, 0x71, 0x7B, 0x74, 0x81, 0x7E, 0x3A, 0x73, 0x74, 0x3A, 0x74, 0x82, 0x75, 0x74,
                            0x81, 0x70, 0x82, 0x3A, 0x71, 0xBB, 0x82, 0x78, 0x72, 0x7E, 0x48, 0x03, 0x07, 0x42, 0x10, 0x31, 0x63, 0x70, 0x71, 0x7B, 0x74, 0x81, 0x7E, 0x3A, 0x73, 0x74, 0x3A, 0x74, 0x82, 0x75, 0x74, 0x81, 0x70, 0x82, 0x3A, 0x70,
                            0x85, 0x70, 0x7D, 0x89, 0x70, 0x73, 0x7E, 0x48, 0x00, 0x09, 0x30, 0x57, 0x70, 0x82, 0x3A, 0x74, 0x7B, 0x74, 0x76, 0x78, 0x73, 0x7E, 0x3A, 0x74, 0x7B, 0x3A, 0x0A, 0x88, 0x83, 0x70, 0x71, 0x7B, 0x74, 0x81, 0x7E, 0x3A,
                            0x73, 0x74, 0x3A, 0x74, 0x82, 0x75, 0x74, 0x81, 0x70, 0x82, 0x3A, 0x71, 0xBB, 0x82, 0x78, 0x72, 0x7E, 0x0A, 0x41, 0x48, 0x03, 0xA2, 0x54, 0x82, 0x83, 0xBB, 0x82, 0x3A, 0x82, 0x74, 0x76, 0x84, 0x81, 0x7E, 0x4F, 0x03,
                            0x42, 0x5D, 0x7E, 0x3A, 0x7F, 0x7E, 0x73, 0x81, 0xBB, 0x82, 0x3A, 0x72, 0x70, 0x7C, 0x71, 0x78, 0x70, 0x81, 0x7B, 0x7E, 0x3A, 0x73, 0x84, 0x81, 0x70, 0x7D, 0x83, 0x74, 0x3A, 0x74, 0x7B, 0x3A, 0x79, 0x84, 0x74, 0x76,
                            0x7E, 0x48, 0x43, 0x03, 0x03, 0x07, 0x31, 0x10, 0x30, 0x62, 0xC4, 0x48, 0x07, 0xE2, 0x03, 0x07, 0x31, 0x10, 0x31, 0x5D, 0x7E, 0x48, 0x00, 0x09, 0x30, 0x57, 0x70, 0x82, 0x3A, 0x74, 0x7B, 0x74, 0x76, 0x78, 0x73, 0x7E,
                            0x3A, 0x74, 0x7B, 0x3A, 0x0A, 0x88, 0x83, 0x70, 0x71, 0x7B, 0x74, 0x81, 0x7E, 0x3A, 0x73, 0x74, 0x3A, 0x74, 0x82, 0x75, 0x74, 0x81, 0x70, 0x82, 0x3A, 0x70, 0x85, 0x70, 0x7D, 0x89, 0x70, 0x73, 0x7E, 0x0A, 0x41, 0x48,
                            0x03, 0xA2, 0x54, 0x82, 0x83, 0xBB, 0x82, 0x3A, 0x82, 0x74, 0x76, 0x84, 0x81, 0x7E, 0x4F, 0x03, 0x42, 0x5D, 0x7E, 0x3A, 0x7F, 0x7E, 0x73, 0x81, 0xBB, 0x82, 0x3A, 0x72, 0x70, 0x7C, 0x71, 0x78, 0x70, 0x81, 0x7B, 0x7E,
                            0x3A, 0x73, 0x84, 0x81, 0x70, 0x7D, 0x83, 0x74, 0x3A, 0x74, 0x7B, 0x3A, 0x79, 0x84, 0x74, 0x76, 0x7E, 0x48, 0x43, 0x03, 0x03, 0x07, 0x31, 0x10, 0x30, 0x62, 0xC4, 0x48, 0x07, 0xE2, 0x03, 0x07, 0x31, 0x10, 0x31, 0x5D,
                            0x7E, 0x48, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x5C, 0xCD, 0x82, 0x78, 0x72, 0x70, 0x00, 0x09, 0x30, 0xA2, 0x60, 0x84, 0xC0, 0x3A, 0x71, 0x70, 0x7D, 0x73, 0x70, 0x3A, 0x82, 0x7E, 0x7D, 0x7E, 0x81, 0x70, 0x3A, 0x7F, 0x81,
                            0x74, 0x75, 0x78, 0x74, 0x81, 0x74, 0x82, 0x4F, 0x03, 0x07, 0x4B, 0x10, 0x30, 0x50, 0x81, 0x81, 0x74, 0x76, 0x7B, 0x70, 0x73, 0x70, 0x03, 0x07, 0x4B, 0x10, 0x31, 0x5E, 0x81, 0x78, 0x76, 0x78, 0x7D, 0x70, 0x7B, 0x00,
                            0x09, 0x30, 0x6A, 0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x62, 0xC4, 0x48, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x48, 0x00, 0x09, 0x30, 0x6A,
                            0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x62, 0xC4, 0x48, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x48, 0x00, 0x09, 0x30, 0x09,
                            0x30, 0xA2, 0x60, 0x84, 0x78, 0x74, 0x81, 0x74, 0x82, 0x3A, 0x84, 0x83, 0x78, 0x7B, 0x78, 0x89, 0x70, 0x81, 0x3A, 0x7B, 0x70, 0x3A, 0x84, 0x7D, 0x78, 0x73, 0x70, 0x73, 0x3A, 0x73, 0x74, 0x3A, 0x73, 0x78, 0x82, 0x72,
                            0x7E, 0x3A, 0x73, 0x84, 0x81, 0x7E, 0x4F, 0x03, 0x10, 0x30, 0x62, 0xC4, 0x48, 0x03, 0x10, 0x31, 0x5D, 0x7E, 0x48, 0x00 },
            // German
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x7B, 0x00, 0x00, 0x00, 0x7B, 0x00, 0x00, 0x00, 0x9C, 0x00, 0x00, 0x02, 0x9C, 0x00, 0x00, 0x02, 0x17, 0x01, 0x00, 0x02, 0x17, 0x01, 0x00, 0x02, 0x94, 0x01, 0x00, 0x02,
                            0x94, 0x01, 0x00, 0x02, 0x0E, 0x02, 0x00, 0x00, 0x0E, 0x02, 0x00, 0x00, 0x23, 0x02, 0x00, 0x02, 0x23, 0x02, 0x00, 0x02, 0x5E, 0x02, 0x00, 0x02, 0x5E, 0x02, 0x00, 0x02, 0x9E, 0x02, 0x00, 0x02, 0x9E, 0x02, 0x00, 0x02,
                            0xDE, 0x02, 0x00, 0x02, 0xDE, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x07, 0xE4, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x07, 0xE4, 0x10, 0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x50, 0x84, 0x82, 0x86, 0x70, 0x77, 0x7B, 0x3A, 0x73, 0x74, 0x82, 0x3A, 0x52, 0x77, 0x70, 0x81, 0x70,
                            0x7A, 0x83, 0x74, 0x81, 0x70, 0x84, 0x75, 0x71, 0x70, 0x84, 0x82, 0x00, 0x09, 0x30, 0x66, 0xBD, 0x77, 0x7B, 0x74, 0x7D, 0x3A, 0x62, 0x78, 0x74, 0x3A, 0x79, 0x74, 0x83, 0x89, 0x83, 0x3A, 0x73, 0x74, 0x7D, 0x3A, 0x63,
                            0x88, 0x7F, 0x3A, 0x73, 0x74, 0x82, 0x3A, 0x52, 0x77, 0x70, 0x81, 0x70, 0x7A, 0x83, 0x74, 0x81, 0x70, 0x84, 0x75, 0x71, 0x70, 0x84, 0x82, 0x4A, 0x03, 0x03, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0x30, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x47, 0x62, 0x7F, 0x77, 0xBD, 0x81, 0x7E, 0x71, 0x81, 0x74, 0x83, 0x83, 0x03, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0x31, 0x5F, 0x81, 0x7E, 0x75, 0x78, 0x47, 0x62, 0x7F, 0x77, 0xBD, 0x81, 0x7E, 0x71, 0x81, 0x74, 0x83, 0x83, 0x00, 0x09, 0x30, 0x62, 0x78, 0x74, 0x3A, 0x77, 0x70, 0x71,
                            0x74, 0x7D, 0x3A, 0x73, 0x70, 0x82, 0x3A, 0x0A, 0x88, 0x62, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x47, 0x62, 0x7F, 0x77, 0xBD, 0x81, 0x7E, 0x71, 0x81, 0x74, 0x83, 0x83, 0x0A, 0x41, 0x3A, 0x70, 0x84, 0x82, 0x76,
                            0x74, 0x86, 0xBD, 0x77, 0x7B, 0x83, 0x48, 0x03, 0x42, 0x58, 0x7C, 0x3A, 0x62, 0x7F, 0x78, 0x74, 0x7B, 0x3A, 0x7A, 0x70, 0x7D, 0x7D, 0x3A, 0x7D, 0x78, 0x72, 0x77, 0x83, 0x3A, 0x7C, 0x74, 0x77, 0x81, 0x3A, 0x84, 0x7C,
                            0x76, 0x74, 0x86, 0xBD, 0x77, 0x7B, 0x83, 0x3A, 0x86, 0x74, 0x81, 0x73, 0x74, 0x7D, 0x3B, 0x43, 0x03, 0x61, 0x74, 0x72, 0x77, 0x83, 0x3A, 0x82, 0x7E, 0x4F, 0x03, 0x03, 0x07, 0x59, 0x10, 0x30, 0x59, 0x70, 0x03, 0x07,
                            0x59, 0x10, 0x31, 0x5D, 0x74, 0x78, 0x7D, 0x00, 0x09, 0x30, 0x62, 0x78, 0x74, 0x3A, 0x77, 0x70, 0x71, 0x74, 0x7D, 0x3A, 0x73, 0x70, 0x82, 0x3A, 0x0A, 0x88, 0x5F, 0x81, 0x7E, 0x75, 0x78, 0x47, 0x62, 0x7F, 0x77, 0xBD,
                            0x81, 0x7E, 0x71, 0x81, 0x74, 0x83, 0x83, 0x0A, 0x41, 0x3A, 0x70, 0x84, 0x82, 0x76, 0x74, 0x86, 0xBD, 0x77, 0x7B, 0x83, 0x48, 0x03, 0x42, 0x58, 0x7C, 0x3A, 0x62, 0x7F, 0x78, 0x74, 0x7B, 0x3A, 0x7A, 0x70, 0x7D, 0x7D,
                            0x3A, 0x7D, 0x78, 0x72, 0x77, 0x83, 0x3A, 0x7C, 0x74, 0x77, 0x81, 0x3A, 0x84, 0x7C, 0x76, 0x74, 0x86, 0xBD, 0x77, 0x7B, 0x83, 0x3A, 0x86, 0x74, 0x81, 0x73, 0x74, 0x7D, 0x3B, 0x43, 0x03, 0x61, 0x74, 0x72, 0x77, 0x83,
                            0x3A, 0x82, 0x7E, 0x4F, 0x03, 0x03, 0x07, 0x58, 0x10, 0x30, 0x59, 0x70, 0x03, 0x07, 0x58, 0x10, 0x31, 0x5D, 0x74, 0x78, 0x7D, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x57, 0x78, 0x7D, 0x83, 0x74, 0x81, 0x76, 0x81, 0x84, 0x7D,
                            0x73, 0x7C, 0x84, 0x82, 0x78, 0x7A, 0x00, 0x09, 0x30, 0x51, 0x78, 0x83, 0x83, 0x74, 0x3A, 0x73, 0x78, 0x74, 0x3A, 0x5C, 0x84, 0x82, 0x78, 0x7A, 0x85, 0x74, 0x81, 0x82, 0x78, 0x7E, 0x7D, 0x3A, 0x86, 0xBD, 0x77, 0x7B,
                            0x74, 0x7D, 0x4A, 0x03, 0x07, 0x4B, 0x10, 0x30, 0x61, 0x74, 0x7C, 0x70, 0x82, 0x83, 0x74, 0x81, 0x03, 0x07, 0x4B, 0x10, 0x31, 0x5E, 0x81, 0x78, 0x76, 0x78, 0x7D, 0x70, 0x7B, 0x00, 0x09, 0x30, 0x6A, 0x52, 0x84, 0x83,
                            0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x59, 0x70, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x74, 0x78, 0x7D, 0x00, 0x09, 0x30, 0x6A,
                            0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x59, 0x70, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x74, 0x78, 0x7D, 0x00, 0x09, 0x30, 0x09,
                            0x30, 0x64, 0x82, 0x74, 0x3A, 0x83, 0x77, 0x74, 0x3A, 0x57, 0x53, 0x53, 0x4F, 0x03, 0x10, 0x30, 0x68, 0x74, 0x82, 0x03, 0x10, 0x31, 0x5D, 0x7E, 0x00 },
            // Italian
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x77, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x84, 0x00, 0x00, 0x02, 0x84, 0x00, 0x00, 0x02, 0xD4, 0x00, 0x00, 0x02, 0xD4, 0x00, 0x00, 0x02, 0x48, 0x01, 0x00, 0x02,
                            0x48, 0x01, 0x00, 0x02, 0xBA, 0x01, 0x00, 0x00, 0xBA, 0x01, 0x00, 0x00, 0xCD, 0x01, 0x00, 0x02, 0xCD, 0x01, 0x00, 0x02, 0x10, 0x02, 0x00, 0x02, 0x10, 0x02, 0x00, 0x02, 0x4E, 0x02, 0x00, 0x02, 0x4E, 0x02, 0x00, 0x02,
                            0x8B, 0x02, 0x00, 0x02, 0x8B, 0x02, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A,
                            0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x62, 0x65, 0x58, 0x5B, 0x64, 0x5F, 0x5F, 0x5E, 0x00, 0x09, 0x30, 0x62, 0x74, 0x7B, 0x74, 0x89, 0x78, 0x7E, 0x7D, 0x70, 0x3A,
                            0x84, 0x7D, 0x70, 0x3A, 0x82, 0x75, 0x74, 0x81, 0x7E, 0x76, 0x81, 0x70, 0x75, 0x78, 0x70, 0x48, 0x03, 0x03, 0x3A, 0x3A, 0x3A, 0x10, 0x30, 0x62, 0x75, 0x74, 0x81, 0x7E, 0x76, 0x81, 0x70, 0x75, 0x78, 0x70, 0x3A, 0x82,
                            0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x03, 0x3A, 0x3A, 0x3A, 0x10, 0x31, 0x62, 0x75, 0x74, 0x81, 0x7E, 0x76, 0x81, 0x70, 0x75, 0x78, 0x70, 0x3A, 0x7C, 0x70, 0x82, 0x83, 0x74, 0x81, 0x00, 0x09, 0x30, 0x50, 0x73,
                            0x7E, 0x83, 0x83, 0x78, 0x3A, 0x7B, 0x70, 0x3A, 0x0A, 0x88, 0x62, 0x75, 0x74, 0x81, 0x7E, 0x76, 0x81, 0x70, 0x75, 0x78, 0x70, 0x3A, 0x82, 0x83, 0x70, 0x7D, 0x73, 0x70, 0x81, 0x73, 0x0A, 0x41, 0x4F, 0x03, 0x42, 0x5D,
                            0x7E, 0x7D, 0x3A, 0x7F, 0x84, 0x7E, 0x78, 0x3A, 0x72, 0x70, 0x7C, 0x71, 0x78, 0x70, 0x81, 0x74, 0x3A, 0x78, 0x7B, 0x3A, 0x7C, 0x7E, 0x73, 0x74, 0x7B, 0x7B, 0x7E, 0x03, 0x73, 0x78, 0x3A, 0x82, 0x85, 0x78, 0x7B, 0x84,
                            0x7F, 0x7F, 0x7E, 0x3A, 0x73, 0x84, 0x81, 0x70, 0x7D, 0x83, 0x74, 0x3A, 0x78, 0x7B, 0x3A, 0x76, 0x78, 0x7E, 0x72, 0x7E, 0x3B, 0x43, 0x03, 0x03, 0x07, 0x31, 0x10, 0x30, 0x62, 0xC3, 0x07, 0xE2, 0x03, 0x07, 0x31, 0x10,
                            0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x50, 0x73, 0x7E, 0x83, 0x83, 0x78, 0x3A, 0x7B, 0x70, 0x3A, 0x0A, 0x88, 0x62, 0x75, 0x74, 0x81, 0x7E, 0x76, 0x81, 0x70, 0x75, 0x78, 0x70, 0x3A, 0x7C, 0x70, 0x82, 0x83, 0x74, 0x81,
                            0x0A, 0x41, 0x4F, 0x03, 0x42, 0x5D, 0x7E, 0x7D, 0x3A, 0x7F, 0x84, 0x7E, 0x78, 0x3A, 0x72, 0x70, 0x7C, 0x71, 0x78, 0x70, 0x81, 0x74, 0x3A, 0x78, 0x7B, 0x3A, 0x7C, 0x7E, 0x73, 0x74, 0x7B, 0x7B, 0x7E, 0x03, 0x73, 0x78,
                            0x3A, 0x82, 0x85, 0x78, 0x7B, 0x84, 0x7F, 0x7F, 0x7E, 0x3A, 0x73, 0x84, 0x81, 0x70, 0x7D, 0x83, 0x74, 0x3A, 0x78, 0x7B, 0x3A, 0x76, 0x78, 0x7E, 0x72, 0x7E, 0x3B, 0x43, 0x03, 0x03, 0x07, 0x31, 0x10, 0x30, 0x62, 0xC3,
                            0x07, 0xE2, 0x03, 0x07, 0x31, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x52, 0x7E, 0x7B, 0x7E, 0x7D, 0x7D, 0x70, 0x3A, 0x82, 0x7E, 0x7D, 0x7E, 0x81, 0x70, 0x00, 0x09, 0x30, 0x62, 0x72, 0x74, 0x76, 0x7B,
                            0x78, 0x3A, 0x78, 0x7B, 0x3A, 0x83, 0x78, 0x7F, 0x7E, 0x3A, 0x73, 0x78, 0x3A, 0x72, 0x7E, 0x7B, 0x7E, 0x7D, 0x7D, 0x70, 0x3A, 0x82, 0x7E, 0x7D, 0x7E, 0x81, 0x70, 0x48, 0x03, 0x07, 0x4B, 0x10, 0x30, 0x61, 0x78, 0x70,
                            0x81, 0x81, 0x70, 0x7D, 0x76, 0x78, 0x70, 0x83, 0x70, 0x03, 0x07, 0x4B, 0x10, 0x31, 0x5E, 0x81, 0x78, 0x76, 0x78, 0x7D, 0x70, 0x7B, 0x74, 0x00, 0x09, 0x30, 0x6A, 0x52, 0x84, 0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A,
                            0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x62, 0xC3, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x6A, 0x52, 0x84,
                            0x83, 0x82, 0x72, 0x74, 0x7D, 0x74, 0x3A, 0x62, 0x7A, 0x78, 0x7F, 0x3A, 0x61, 0x74, 0x7C, 0x7E, 0x85, 0x74, 0x81, 0x3A, 0x85,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x6C, 0x3A, 0x03, 0x07, 0x4B, 0x62, 0x83, 0x70, 0x81, 0x83, 0x3A, 0x76, 0x70, 0x7C, 0x74, 0x4F, 0x03, 0x07, 0x4F, 0x10, 0x30, 0x62, 0xC3, 0x03, 0x07, 0x4F, 0x10, 0x31, 0x5D, 0x7E, 0x00, 0x09, 0x30, 0x64, 0x82, 0x78,
                            0x3A, 0x78, 0x7B, 0x3A, 0x73, 0x78, 0x82, 0x72, 0x7E, 0x3A, 0x81, 0x78, 0x76, 0x78, 0x73, 0x7E, 0x4F, 0x03, 0x10, 0x30, 0x62, 0xC3, 0x03, 0x10, 0x31, 0x5D, 0x7E, 0x00 }, // Italian
            new byte[] {  },
            new byte[] {  },
            new byte[] {  },
            // Korean
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x6F, 0x00, 0x00, 0x00, 0x6F, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x02, 0x80, 0x00, 0x00, 0x02, 0xD1, 0x00, 0x00, 0x02, 0xD1, 0x00, 0x00, 0x02, 0xFD, 0x00, 0x00, 0x02,
                            0xFD, 0x00, 0x00, 0x02, 0x2C, 0x01, 0x00, 0x00, 0x2C, 0x01, 0x00, 0x00, 0x3A, 0x01, 0x00, 0x02, 0x3A, 0x01, 0x00, 0x02, 0x66, 0x01, 0x00, 0x02, 0x66, 0x01, 0x00, 0x02, 0xA5, 0x01, 0x00, 0x02, 0xA5, 0x01, 0x00, 0x02,
                            0xE4, 0x01, 0x00, 0x02, 0xE4, 0x01, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10,
                            0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0x3A, 0xEA, 0x2D, 0x58, 0x4E, 0x00, 0x09, 0x30, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0xA8, 0x3A, 0xEA, 0x2D, 0x58, 0x95, 0xC3, 0x3A,
                            0xD2, 0x2C, 0x50, 0xA2, 0xB6, 0x3B, 0x3A, 0x03, 0x42, 0x2D, 0xC5, 0x2C, 0xEF, 0x8E, 0x3A, 0x2C, 0x63, 0x96, 0x91, 0x3A, 0x2D, 0xAE, 0x2C, 0x6C, 0xD9, 0x3A, 0xAE, 0x3A, 0xB9, 0xBA, 0x98, 0x8F, 0x43, 0x03, 0x3A, 0x10,
                            0x30, 0x9C, 0x2C, 0x9B, 0x3A, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0x03, 0x3A, 0x10, 0x31, 0xCB, 0x2E, 0x4D, 0xB4, 0x2C, 0x43, 0x3A, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0x00, 0x09, 0x30, 0x0A, 0x88, 0x4C, 0x9C, 0x2C,
                            0x9B, 0x3A, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0x4E, 0x0A, 0x41, 0x03, 0x3A, 0x2C, 0x7A, 0x2C, 0x9C, 0xBE, 0xBA, 0x98, 0xAC, 0x4F, 0x3A, 0x03, 0x3A, 0x10, 0x30, 0x2C, 0xF3, 0x03, 0x3A, 0x10, 0x31, 0x94, 0x98, 0xBD,
                            0x00, 0x09, 0x30, 0x0A, 0x88, 0x4C, 0xCB, 0x2E, 0x4D, 0xB4, 0x2C, 0x43, 0x3A, 0xC0, 0xFE, 0x92, 0x3A, 0xBB, 0xC9, 0x4E, 0x0A, 0x41, 0x03, 0x3A, 0x2C, 0x7A, 0x2C, 0x9C, 0xBE, 0xBA, 0x98, 0xAC, 0x4F, 0x3A, 0x03, 0x3A,
                            0x10, 0x30, 0x2C, 0xF3, 0x03, 0x3A, 0x10, 0x31, 0x94, 0x98, 0xBD, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0x5B, 0x60, 0x66, 0x3A, 0x2C, 0x84, 0xCE, 0x4E, 0x00, 0x09, 0x30, 0x5B, 0x60, 0x66, 0x3A, 0x2C, 0x6A, 0xE4, 0x93,
                            0x3A, 0x3A, 0xEA, 0x2D, 0x58, 0x9F, 0x3A, 0xD2, 0x2C, 0x50, 0xA2, 0xB6, 0x3B, 0x03, 0x07, 0x4B, 0x10, 0x30, 0x92, 0x2C, 0xEF, 0xDE, 0x90, 0x03, 0x07, 0x4B, 0x10, 0x31, 0xB6, 0xA4, 0x90, 0x2E, 0xE0, 0x00, 0x09, 0x30,
                            0x4C, 0x5C, 0x88, 0x87, 0x86, 0x76, 0x78, 0x81, 0x78, 0x3A, 0x6C, 0x7E, 0x7C, 0x83, 0x3A, 0x6B, 0x78, 0x80, 0x82, 0x89, 0x78, 0x85, 0x3A, 0x89,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x6C, 0x87, 0x74, 0x85, 0x87, 0x3A, 0x60, 0x74, 0x80, 0x78, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0x2C, 0xF3, 0x03, 0x07, 0x50, 0x10, 0x31, 0x94, 0x98, 0xBD, 0x00, 0x09, 0x30, 0x4C, 0x5C,
                            0x88, 0x87, 0x86, 0x76, 0x78, 0x81, 0x78, 0x3A, 0x6C, 0x7E, 0x7C, 0x83, 0x3A, 0x6B, 0x78, 0x80, 0x82, 0x89, 0x78, 0x85, 0x3A, 0x89,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x6C, 0x87, 0x74, 0x85, 0x87, 0x3A, 0x60, 0x74, 0x80, 0x78, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0x2C, 0xF3, 0x03, 0x07, 0x50, 0x10, 0x31, 0x94, 0x98, 0xBD, 0x00, 0x09, 0x30, 0x95, 0xC9,
                            0x2C, 0x57, 0xC0, 0x2C, 0xD4, 0x3A, 0xC9, 0xA9, 0x8E, 0x2D, 0x40, 0xA8, 0x3A, 0xA1, 0x2C, 0x43, 0x95, 0xBE, 0xBA, 0x98, 0xAC, 0x4F, 0x03, 0x10, 0x30, 0x2C, 0xF3, 0x03, 0x10, 0x31, 0x94, 0x98, 0xBD, 0x00 },
            // Chinese
            new byte[] { 0x50, 0x00, 0x00, 0x03, 0x50, 0x00, 0x00, 0x03, 0x6F, 0x00, 0x00, 0x00, 0x6F, 0x00, 0x00, 0x00, 0x7C, 0x00, 0x00, 0x02, 0x7C, 0x00, 0x00, 0x02, 0xAD, 0x00, 0x00, 0x02, 0xAD, 0x00, 0x00, 0x02, 0xD7, 0x00, 0x00, 0x02,
                            0xD7, 0x00, 0x00, 0x02, 0xFF, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00, 0x0C, 0x01, 0x00, 0x02, 0x0C, 0x01, 0x00, 0x02, 0x31, 0x01, 0x00, 0x02, 0x31, 0x01, 0x00, 0x02, 0x6E, 0x01, 0x00, 0x02, 0x6E, 0x01, 0x00, 0x02,
                            0xAB, 0x01, 0x00, 0x02, 0xAB, 0x01, 0x00, 0x02, 0x09, 0x30, 0x10, 0x30, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10, 0xFF, 0x03, 0x10, 0x31, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x3A, 0x10,
                            0xFF, 0x03, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0xB1, 0x2C, 0xA8, 0xB8, 0xA8, 0xC8, 0x4E, 0x00, 0x09, 0x30, 0x2C, 0x90, 0xB1, 0x2C, 0xA8, 0xB8, 0xA8, 0xC8, 0x3B, 0x03, 0x3A, 0x42, 0x2F, 0x84, 0x2F, 0x85, 0x9B, 0x90,
                            0x91, 0x2E, 0x5E, 0x2E, 0xE6, 0x43, 0x03, 0x3A, 0x10, 0x30, 0x04, 0x2E, 0xF2, 0x2F, 0xB7, 0xB8, 0xA8, 0xC8, 0x03, 0x3A, 0x10, 0x31, 0xFE, 0x2F, 0xA4, 0xB8, 0xA8, 0xC8, 0x00, 0x09, 0x30, 0x0A, 0x88, 0x4C, 0x04, 0x2E,
                            0xF2, 0x2F, 0xB7, 0xB8, 0xA8, 0xC8, 0x4E, 0x0A, 0x41, 0x03, 0x3A, 0x2C, 0xAC, 0xFF, 0x2C, 0x3F, 0x2E, 0x38, 0x2C, 0xC0, 0x2D, 0xAC, 0x4F, 0x03, 0x3A, 0x10, 0x30, 0xF7, 0x03, 0x3A, 0x10, 0x31, 0x2E, 0x69, 0x00, 0x09,
                            0x30, 0x0A, 0x88, 0x4C, 0xFE, 0x2F, 0xA4, 0xB8, 0xA8, 0xC8, 0x4E, 0x0A, 0x41, 0x03, 0x3A, 0x2C, 0xAC, 0xFF, 0x2C, 0x3F, 0x2E, 0x38, 0x2C, 0xC0, 0x2D, 0xAC, 0x4F, 0x03, 0x3A, 0x10, 0x30, 0xF7, 0x03, 0x3A, 0x10, 0x31,
                            0x2E, 0x69, 0x00, 0x09, 0x30, 0x0A, 0x43, 0x4C, 0x2D, 0xA8, 0xFF, 0x5B, 0x60, 0x66, 0x4E, 0x00, 0x09, 0x30, 0x2C, 0x90, 0xB1, 0x2C, 0xA8, 0x5B, 0x60, 0x66, 0x8E, 0x2F, 0xE4, 0x2E, 0xAB, 0x3B, 0x03, 0x07, 0x45, 0x10,
                            0x30, 0x2C, 0xCC, 0x2A, 0x30, 0x2E, 0xDF, 0x03, 0x07, 0x45, 0x10, 0x31, 0x2C, 0xA2, 0x2E, 0xDF, 0x00, 0x09, 0x30, 0x4C, 0x5C, 0x88, 0x87, 0x86, 0x76, 0x78, 0x81, 0x78, 0x3A, 0x6C, 0x7E, 0x7C, 0x83, 0x3A, 0x6B, 0x78,
                            0x80, 0x82, 0x89, 0x78, 0x85, 0x3A, 0x89,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x6C, 0x87, 0x74, 0x85, 0x87, 0x3A, 0x60, 0x74, 0x80, 0x78, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0xF7, 0x03, 0x07, 0x50, 0x10, 0x31, 0x2E, 0x69, 0x00, 0x09, 0x30, 0x4C, 0x5C, 0x88, 0x87,
                            0x86, 0x76, 0x78, 0x81, 0x78, 0x3A, 0x6C, 0x7E, 0x7C, 0x83, 0x3A, 0x6B, 0x78, 0x80, 0x82, 0x89, 0x78, 0x85, 0x3A, 0x89,
                            majorID, 0x48, minorID, 0x48, patchID,
                            0x4E, 0x3A, 0x03, 0x07, 0x4C, 0x6C, 0x87, 0x74, 0x85, 0x87, 0x3A, 0x60, 0x74, 0x80, 0x78, 0x4F, 0x03, 0x07, 0x50, 0x10, 0x30, 0xF7, 0x03, 0x07, 0x50, 0x10, 0x31, 0x2E, 0x69, 0x00, 0x09, 0x30, 0x2C, 0x3F, 0x93, 0x94,
                            0x2D, 0x33, 0x04, 0x2C, 0xFB, 0x2D, 0xAC, 0x4F, 0x03, 0x10, 0x30, 0xF7, 0x03, 0x10, 0x31, 0x2E, 0x69, 0x00 }
        };

        public override void Execute(string defaultDescription = "")
        {
            byte language = base.memoryWatchers.Language.Current;
            byte[] NewGameBytes = NewGameText[language];

            new Transition { ForceLoad = false, Description = "New Game - Version Information", DialogueFile = NewGameBytes }.Execute();

        }
    }
}

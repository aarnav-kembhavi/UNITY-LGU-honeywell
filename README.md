# LGU-Fusion Coolant-Level Indicator  
*A non-intrusive liquid-level system that fuses laser-generated ultrasound (LGU) and thermal imaging.*

---

## Table of Contents
1. [Motivation & Requirements](#1-motivation--requirements)  
2. [High-Level Architecture](#2-high-level-architecture)  
3. [Unity Digital-Twin (Phase-0)](#3-unity-digital-twin-phase-0)  
4. [Physical Hardware (Phase-1)](#4-physical-hardware-phase-1)  
5. [3-D-Printable Mechanics](#5-3-d-printable-mechanics)  

---

## 1. Motivation & Requirements
* Ground crew spend ≈ 12 min per flight manually dip-checking the avionics-coolant bottle in a hot, cramped bay.  
* Tank is sealed, painted aluminium (≈ 3 bar); probes, sight-glasses and grease-coupled ultrasonics are unacceptable.  
* Target spec: **± 2 mm accuracy**, –50 → +120 °C, retrofit in < 15 min, parts cost < $60, zero scheduled maintenance.

---

## 2. High-Level Architecture

| Layer | Key Component | Function |
|-------|---------------|----------|
| Excitation | 905 nm Class-1 diode | 20 ns optical tap through air |
| Sensing | AlN piezo disc | Converts wall strain to voltage |
| | FLIR Lepton | Finds liquid-air temp step |
| Processing | ESP32-S3 | Pulse capture, fusion, MQTT out |
| Power | 5 × 5 cm solar + 15 F super-cap | 10 µA deep-sleep = 10-year life |
| Mechanics | PET-G snap clamp | Mounts sensors—no drilling |
| UI | Node-RED dashboard | Green / amber / red gauge + phone alert |

---

## 3. Unity Digital-Twin (Phase 0)

### Features
* **Zibra Liquids** SPH fluid sim with drain-valve keyboard control.  
* LGU attenuation scripted from Kim 2020 (air vs liquid damping).  
* **Thermal camera sensor** (Unity Simulation Sensors) outputs 80 × 60 frames.  
* **Kalman filter** fuses LGU + IR → RMS error **1.4 mm** under randomised noise & paint thickness.  
* MQTT publisher streams `{level_mm, health}` to local broker for Node-RED / Grafana UI.

### Why a twin?
* Lets us tune algorithms and dashboards **before** hardware exists.  
* Generates labelled data for fast calibration.  
* Randomises temperature, noise and paint to de-risk real-world variability.

---

## 4. Physical Hardware (Phase 1)

| Block | Part | Note |
|-------|------|------|
| Laser | Osram SPL PL90 diode + gate driver | Class-1 enclosed; 20 ns pulse |
| Receiver | 20 mm AlN piezo + OPA356 pre-amp | Survives heat & radiation |
| MCU | ESP32-S3-Mini-1 | Wi-Fi/BLE, 8 MB PSRAM |
| Power | Flex PV 5 V/200 mA + 15 F super-cap | < 10 µA deep-sleep draw |
| Housing | PET-G clamp & shroud | Prints in < 1 h; no supports |

---

## 5. 3-D-Printable Mechanics

| STL file | Print spec | Purpose |
|----------|------------|---------|
| `clamp_shell.stl` | PET-G · 0.2 mm · 20 % | Wraps tank; holds laser, piezo, wiring |
| `laser_cartridge.stl` | PET-G · 0.15 mm · 100 % | Snaps diode at correct standoff |
| `piezo_saddle.stl` | PET-G · 0.2 mm | Centers & isolates piezo |
| `light_shroud.stl` | PET-G · black · 0.2 mm | Blocks stray beam (Class-1) |
| `alignment_jig_80mm.stl` | PLA · 0.3 mm | Ensures repeatable clamp height |

All parts have < 45° overhangs, so **no supports** or post-processing are needed. Total plastic mass ≈ 35 g; print time for the full set ≈ 90 minutes on any desktop FDM printer.

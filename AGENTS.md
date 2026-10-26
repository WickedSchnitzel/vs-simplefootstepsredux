# Simple Footsteps Redux – Projekt-Notizen

> **Für Agents: Erst diesen Block komplett lesen.** Oberste Regel: **NIEMALS raten.**
> Verhalten (Engine, Vanilla, eigener Code) immer an vertrauenswürdiger Quelle
> verifizieren: 1. Projekt-Source, 2. decompilierte VS-DLLs (`ilspycmd`), 3. offizielle
> Doku/Wiki. Installierte 1.22.7-DLLs: `%VINTAGE_STORY%\*.dll` (`VintagestoryAPI.dll`,
> `VintagestoryLib.dll`, `Mods\VSEssentials.dll` — dort liegen die `AiTask*`-Klassen,
> `Vintagestory.GameContent`).

## Was dieses Projekt ist

- C#-Code-Mod für **Vintage Story 1.22.7** (Dedicated Multiplayer, aber auch SP-fähig:
  `modinfo.json` hat `RequiredOnClient` + `RequiredOnServer` = true).
- **Eine DLL**, ein csproj: `simplefootstepsredux.csproj` → `simplefootstepsredux.dll`,
  Target `net10.0` (VS 1.22 läuft auf .NET 10 — 1.21 war net8.0, die DLL ist NICHT
  abwärtskompatibel, daher `dependencies.game = "1.22.0"`).
- Zweck: Fußstapfen-Sounds für Entities. Whitelist-Ansatz: nur explizit gelistete
  Mob-Codes bekommen Sounds (frühere "Simple Footsteps"-Version galt für alle).
- Upstream-Herkunft: QPTech/MisterAndyDandy, für CoB (Kai). Release-Plattform:
  mods.vintagestory.at/simplefootstepsredux. Git-Remote:
  `https://github.com/WickedSchnitzel/vs-simplefootstepsredux.git` (Branch `master`).
- Stand 1.0.6 (Okt 2026): Update 1.21.6 → 1.22.7. Lokaler Stand war identisch zum
  ModDB-Release 1.0.5 (Modinfo+Config byte-gleich, DLL-MD5 `e335b8aa` matchend).

## Funktionsweise (verifiziert an Source + VSEssentials-Decompile)

- `src/simplefootstepsreduxModSystem.cs`, `Start()`: `AiTaskRegistry.Register<AiTaskLoudX>(code)`
  überschreibt die Vanilla-Task-Codes → **jede** Entity, die diese AI-Tasks nutzt,
  bekommt die Loud-Variante. Registrierte Codes: `wander`, `fleeentity`,
  `seekentity`, `getoutofwater`, `stayclosetoentity` — alle in 1.22.7 noch in
  Vanilla-Entity-JSONs verwendet (verifiziert).
- `src/ai/aitaskupdates.cs`: 5 Klassen `AiTaskLoud*` erben vom jeweiligen Vanilla-Task.
  In `ContinueExecute` läuft ein `stepTimer`; alle `soundTime` Sekunden
  `world.PlaySoundAt(stepSound, pos, range 32, volume)` — nur wenn ein `SoundEntry`
  matcht (einmalig aufgelöst, `alreadycheckedforsound`).
- `assets/simplefootstepsredux/config/soundentries.json` = Whitelist. Geladen in
  `AssetsFinalize` via `api.Assets.GetMany("config/soundentries.json")` — sammelt
  die Datei aus **allen** Mods (fremde Mods können so ihre eigene beisteuern,
  siehe readme.txt). Format siehe readme.txt.
- **Matching ist SUBSTRING**: `entity.Code.Contains(mobMatchCode)` +
  `soundTrigger` exakt gleich Task-Code. `FirstOrDefault` → **erste Regel in
  Listenreihenfolge gewinnt**. Konsequenzen:
  - Kurze/generische Matchcodes matchen weit (z.B. `trader` trifft Vanilla
    `trader-*` UND CoB-`junktrader` — erwünscht).
  - `deer` matcht auch Elche (`semitameddeer`, `tameddeer` — Code enthält "deer").
  - Beim Hinzufügen immer prüfen: matcht der Code unbeabsichtigt andere Entities?
    Und: würde ein **früherer** Eintrag die neue Entity schon matchen?
- **Tote Matchcodes sind harmlos** (kein Match → kein Sound), aber aufräumen lohnt:
  `-humanoid-trader` wurde durch `trader` ersetzt (Vanilla-Codes hießen früher
  `humanoid-trader-*`, seit 1.2x `trader-{gender}-{type}`).
- **VS 1.22 hat eigene Ambient-Sounds in `AiTaskBase`** eingeführt: `[JsonProperty]
  sound/soundRange/soundStartMs/soundRepeatMs/soundChance` (darüber haben Wolf/Bär
  Vanilla-Fußstapfen). Das ist ein SEPARATER Mechanismus — unsere `stepSound`-Logik
  bleibt eigenständig. **Vorsicht beim Felder benennen**: `soundRange` und `range`
  existieren schon geerbt (CS0108) — eigene Felder nicht so nennen (daher
  `stepSoundRange`).

## Build & Release

1. `dotnet build simplefootstepsredux.sln -c Release` → Erwartung **0 Warnungen,
   0 Fehler**. Output: `bin\Release\Mods\mod\` (DLL + assets + modinfo via
   `<Content Include="assets\**">` + modinfo).
2. `$(VINTAGE_STORY)` env zeigt auf das Spiel-Verzeichnis (aktuell
   `C:\Users\Kai\AppData\Roaming\Vintagestory`, 1.22.7) — alle HintPaths darüber.
3. **Release-Zip-Struktur** (wie ModDB 1.0.5): `modinfo.json` + `simplefootstepsredux.dll`
   + `assets/` flach im Zip-Root. Kai packt/lädt manuell hoch. Vor Release:
   `version` in `modinfo.json` bumpen.
4. `bin/`, `obj/` sind gitignored — DLL nicht committen.

## Git-Fallen

- Remote-Historie (`master`) enthält noch die **alte CakeBuild-Struktur**
  (`simplefootstepsredux/` Unterordner, `build.ps1`, `CakeBuild/`). Der aktuelle
  Stand ist **flach** (`src/`, `assets/`, `modinfo.json` im Root) — der erste
  Update-Commit ersetzt die Struktur komplett. Danach normal weiterarbeiten.
- Ein Commit pro Thema. Nach Änderungen committen + `git push origin master`.

## soundentries.json pflegen (häufigste Aufgabe)

- Vanilla-Entity-Codes ermitteln: `%VINTAGE_STORY%\assets\survival\entities\**\*.json`,
  `code:` + Variant-Gruppen beachten (States werden angehängt, z.B.
  `fox-{type}-adult-male`). Matching testen als Substring über die **aufgelösten**
  Codes.
- Welche Entities Footsteps bekommen sollten: nur die mit Boden-AI-Tasks
  (`wander`/`fleeentity`/`seekentity`/`stayclosetoentity`/`getoutofwater`).
  Grep in Entity-JSONs. Fische nutzen `wander` unter Wasser → keine Steps.
  Fliegendes/Tiny (butterfly, bell, mechhelper, erel, animalbot, playerbot): keine.
- Stand 1.0.6 Vanilla-Abdeckung: shiver, bowtorn, drifter, locust, trader,
  villager, deer(+elk), goat, moose, bear, gazelle, hyena, pig, sheep, wolf,
  fox, hare, raccoon, chicken.
- Nur 3 Soundfiles existieren: `steps/drifter.ogg` (schwer/klumpig),
  `steps/npc.ogg` (leicht/normal), `steps/water.ogg` (getoutofwater/Spritzer).
- Struktur pro Mob = 5 Einträge (einer je soundTrigger). `soundTime` = Abstand
  in Sekunden (kleiner = öfter), `volume` 0–1.3, `changepitch` für Variation.
- Datei ist **relaxed JSON** (//-Kommentare, Trailing-Commas erlaubt) — kein
  strikter Parser zum Prüfen nehmen.

# Nostalgia MUD Engine

**Nostalgia MUD Engine (NME)** is a game engine for multiplayer text-based role-playing games. A *MUD (Multi-User Dungeon)* is an antique game genre that arose in the 1970s and peaked in popularity during the late 1990s. MUDs are the precursors of MMORPGs, inspiring games like EverQuest, World of Warcraft, and Elder Scrolls Online.

Most traditional MUDs are purely text-based. In lieu of graphics, the game world is rendered with text -- like a book. Players interact with the game by entering commands (e.g. "get lamp"). NME relies on modern web technologies to deliver a browser-based experience, eliminating the need for a separate MUD client, while easily delivering visual and audio effects.

### Technology Stack

  - The server is written in C#, using .NET Core 3.1
  - The client was built on React
  - Real-time gameplay is delivered over websockets using SignalR

### Scope
NME is *not* an MMORPG, and lacks the scalability necessary for massive player numbers. Meeting the resource demands of a massive player population would require an architectural overhaul, and is beyond the scope of this project. Given that MUDs are a very niche hobby, and most have less than 50 concurrent players, NME will support most practical use-cases with ease.

### Installation & Customization
Using NME requires some familiarity with its technology stack, which is beyond the scope of this documentation.

### Usage License
Nostalgia MUD Engine is licensed under GNU GPLv3. See COPYING.txt for the full official licensing text. In short, derivatives may be used for commercial purposes, but the source code must be public and must use the same GNU GPLv3 license.

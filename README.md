# LaborTakesTwo

## Team
|Name            | Matr.Nr.|
|----------------|---------|
|Alexander Justus|  202959 |
|Mike Schubert   |  202220 |
|Dennis Weber    |  202394 |

## Besonderheiten des Projekts

Für die Entwicklung haben wir eine Art eigenen Szenenmanager entwickelt. Beim Start des Spiels schaut dieses, von welcher Szene aus es gestartet wurde und lädt diese dann automatisch. Wenn das Spiel allerdings regulär in der "Start"-Szene gestartet wurde, wird eine vorausgewählte Szene geladen (Standardmäßig ist dies die "Tutorial" Szene). Dies hat den Vorteil, dass Entwickler einerseits nicht immer das Spiel bis zur gewünschten Szene spielen müssen, andererseits den Start des Spiels nicht bearbeiten müssen. 

## Besondere Leistungen, Herausforderungen und gesammelte Erfahrungen


### Szenen- / Spawnsystem

Unter den besonderen Leistungen, die wir vollbracht haben, zählen wir vor allem unser Szenenmanager. Dies machte
es deutlich angenehmer für uns in Git/Unity unsere Szenen zu managen und ermöglichte die Arbeit an unterschiedlichen
Stellen im Projekt, ohne das man die anderen Mitarbeiter auf irgendeine Art verlangsamt. Kombiniert mit einem 
Spawnpunktmanager konnten wir außerdem für jede Szene einen anderen Spawnpunkt setzen, dies machte es uns extrem
einfach, bestimmte Mechaniken zu testen, da wir dadurch nicht durch das gesamte Level spielen mussten, um zu testen.
Um über das "PhotonNetwork" Objekte in einer Szene zu spawnen, ist es notwendig, einen Spawnpunkt anzugeben. 
Des weiteren war es ein großes Hindernis, wenn die Spielerposition bei einem Szenenwechsel gleich bleibt.

### Photon

Unsere größte Herausforderung hier war es, unser Spiel an Photon anzupassen. Dazu fanden wir über die gesamte
Projektlaufzeit mehrere Möglichkeiten, Daten über Photon zu übertragen und passten diese je nach Anwendungsbereich
an. Zu den Übertragungsmethoden, die wir verwendet haben, zählen: PhotonEvents, RPCs und Streams (IPunObservable), die alle in jeweils
anderen Bereichen extrem nützlich sind. Um das gesamte Projekt im Multiplayer spielbar zu machen, kostete es uns
ungefähr noch mal die hälfte der Zeit, die wir für die Entwicklung der Mechaniken und Skripte benötigt hatten.

### Waffensystem

Durch das verwenden von Scriptable Objects war es uns möglich, unsere Waffen nach einem Bauplan schnell und einfach
zu erstellen, so hatten alle Waffen einen eigenen Animator, Werte und Daten sowie vordefinierte Methoden, die jede
Waffe implementieren mussten (z. B. primary und secondary attack).

So haben wir sechs verschiedene Waffen zur Verfügung
gestellt.
- Einen Hammer zum zerstören von Steinen und Eisblöcken (Eisstab) oder kann durch aufladen, der Secondary Attacke
  große Objekte wie Steine verschieben und somit Hindernisse aus dem Weg räumen.
- Eine Gießkanne zum Gießen von Pflanzen, hier gibt es zwei verschiedene Arten (eine Schlangenpflanze und eine Laufpflanze),
  außerdem können Pflanzen vom Spieler übernommen werden, sodass er diese Steuern und neue Möglichkeiten hat, das Level zu lösen.
  Die Schlangenpflanze kann z. B. sich in eine Richtung ausbreiten (auch um Kurven herum) und somit Brücken für den anderen Spieler
  bauen. Die Laufpflanze ermöglich dem Spieler, die Pflanze zu steuern und somit durch Wasser zu laufen. Auf dem Kopf der Laufpflanze
  befindet sich ein Blatt, welcher vom nicht Pflanzen Spieler als Plattform verwendet werden kann. Dies war in Photon sehr schwierig
  umzusetzen, da hier der Spieler über das Netzwerk geupdatet werden musste, sowie alles, was die Pflanze an Übergängen erstellt.
- Schrumpfstab, um Objekte wie Steine sowie sich selber oder den Mitspieler zu schrumpfen, es gibt keine Möglichkeit, sich selber
  wieder größer zu machen, außer wenn der andere Spieler den Vergrößerungsstab auf beide anwendet.
- Vergrößerungsstab vergrößert Objekte sowie den Spieler selbst. Kann nur mit dem Schrumpfstab rückgängig gemacht werden.
- Feuerstab kann unteranderem Partikel sowie Projektile verschießen. Das Feuer, welches der Stab wirft, kann dafür
  sorgen, das Eisblöcke wieder schmelzen und zu Wasser werden. Kann gegen den Eisstrahl geschossen werden, was dazu führt, dass aus
  Eis und Feuer Rauch wird. Dieser Rauch kann dazu benutzt werden, Turbinen zu bewegen.
- Eisstab zum Einfrieren von Objekten, welche dann vom Hammer zerstört werden können. Kann auch Eiskugeln als Projektile schießen

Um dem Spieler das wechseln von Waffen zu ermöglichen, haben wir ein Inventarsystem entwickelt, welches es uns ermöglich, durch die
verschiedenen Waffen zu scrollen und während der Laufzeit neue Waffen hinzuzufügen. Das Inventar lädt sich aus den verschiedenen
Waffen nicht nur die Animationen, sondern auch alle wichtigen Skripte und wendet diese auf die Hand des Spielers an.

Für den Spieler und die Waffen haben wir uns eine kleine UI geschrieben. Über dem Spieler werden kleine Herzen angezeigt, welche
dem Spieler seine HP anzeigen. Die Waffen haben jeweils ihren eigenen Informationstexte wenn man über diese im Spiel hovert.
Damit der Spieler nicht jedes Mal erneut durch das ganze Level laufen muss, wenn er stirbt, haben wir über die ganze Map Respawnpunkte
verteilt. Diese sind sehr einfach zu verwenden und können daher ganz einfach platziert werden. Es muss lediglich das Prefab des
Respawnpunktes platziert werden und schon ist es einsatzbereit. Der Spieler muss mit dem Collider des Respawnpunktes kollidieren,
damit dieser als Punkt gespeichert wird.

Zusätzlich haben wir probiert, unsere eigene Inspektor UI für Unity zu entwickeln, die uns die Arbeit für ...
Erleichtert. Leider jedoch hat es uns nicht überzeugen können und wurde daher verworfen bzw. nicht mehr weiter
entwickelt.

## Gesammelte Erfahrungen

Bei folgenden Themen konnten wir besonders viel Erfahrung sammeln (da wir die meisten davon kaum oder nie verwendet haben)
- Animator
- Scriptable Objekts
- Particlesystem
- Blender in Unity
- Photon (sehr steile Lernkurve)
- Unity Inspector UI
- Github und Unity
- Szenenmanipulation

## Selbst erstellte Assets

- Animation (Für den Hammer) 
- Partikel (Verschiedene Schneeflocken, Rauch, Feuerpartikel, sprinkel -und plätscherndes Wasser
- Modell in Blender (Boss Kopf und Hände, Schrumpf und Vergrößerungsstrahler, Feuer und Eisstäbe,
  Gießkanne und Hammer)
- PixelArt (HP für den Spieler)

## Importierte Assets

- https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-pack-free-187052
- https://assetstore.unity.com/packages/3d/environments/landscapes/lowpoly-environment-pack-99479
- https://assetstore.unity.com/packages/3d/environments/landscapes/free-low-poly-nature-forest-205742
- https://assetstore.unity.com/packages/3d/props/exterior/low-poly-brick-houses-131899
- https://assetstore.unity.com/packages/3d/environments/fantasy/battle-cannon-70589
- https://assetstore.unity.com/packages/2d/textures-materials/stone/hand-painted-stone-textures-28645
- https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-volume-2-nebula-3392

## Dokumentation

Hier stehen alle Ideen, Meetings und Präsentationsvorbereitungen
https://docs.google.com/document/d/1hfgJ5XBw0JGtW6MCj486S43_ZLwAsgodCdGVBiRQ-9s/edit?usp=sharing

## Playthrough Video (10min) 

Zeigt das Playthrough aus der Sicht beider Spieler. Video wurde zusammengeschnitten, sodass man jedes
Hindernis und jede Mechanik gut sehen kann. Die vier verschiedenen Level, die wir gebaut haben, sind in der
Beschreibung mit Zeitstempeln gekennzeichnet.
https://youtu.be/gnrs3pYBm4s

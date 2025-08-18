# Aftertone

**Aftertone** is a Unity plugin designed for efficient audio management. t uses a pool of reusable audio sources to provide a fast, lightweight, and robust way of playing short-lived, spatial audio like explosions, footsteps, melee impacts, and more.

# ⚡ Features

## Configurable
Aftertone can be customized via a config asset automatically created in the **Resources** folder.

You can configure:
- ``Initial Pool Size`` - Number of audio sources to pre-create at startup.
- ``Pool Expansion Size`` - Number of audio sources to add when the pool runs out.
- ``Max Pool Size`` - Upper limit for the number of pooled sources.
- ``Use Debug`` - Displays an in-game debug panel showing pool usage.

## Lightweight
The entire system was built to be light weight and as performance optimized as possible.

## SO-Architecture
Sounds are defined using ScriptableObjects called ToneAssets.

Each ToneAsset defines:
- The audio resource (an AudioClip or a random container)
- All relevant properties of a Unity AudioSource (volume, spatial blend, pitch, etc.)

This architecture encourages reuse, configurability, and a clean separation between logic and data.

## Controllable Playback via Handles
When you play a sound, you receive a ToneHandle — a safe, lightweight struct that allows you to:
- Set the 3D position (.At(position))
- Track a Transform for dynamic movement (.Track(transform))
- Pause, resume, or stop playback
- Register completion or stop callbacks

Once the audio finishes or is stopped, the handle is automatically marked as invalid to prevent accidental reuse.

## Custom Editors
Use Unity’s custom inspector to easily configure sound assets. You can:
- Copy properties from existing AudioSource components into a ToneAsset
- Preview or test sounds directly from the editor

# 🧪 Example Use
```csharp
// Play a sound at a position
var handle = Aftertone.Play(explosionAsset).At(transform.position);

// Optionally track a moving object
handle.Track(targetTransform);

// Stop manually if needed
handle.Stop();
```
# ⭐ Collaboration
Want to contribute, fix bugs, or suggest improvements?
Feel free to submit issues, improvements or even pull requests. 
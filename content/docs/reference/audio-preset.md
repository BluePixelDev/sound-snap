# `AudioPreset`

**Namespace:** `BP.Audipool`  
**Type:** `struct`(sealed, `ScriptableObject`)

A reusable, asset-based container for audio configuration.
`AudioPreset` wraps an internal [`AudioSourceData`][AudioSourceData] instance and exposes
methods to copy from or apply settings to a Unity `AudioSource`.

Tone assets are intended for **authoring audio presets in the editor** and
reusing them consistently at runtime.

## Description

`AudioPreset` allows audio configuration to be stored as a Unity asset rather
than constructed at runtime. This makes it suitable for:

* Designer-authored audio presets
* Consistent configuration across multiple sounds
* Integration with `AudioHandle.WithAsset(...)`

Internally, the asset delegates all behavior to an `AudioSourceData`
instance.

## Methods

### `CopyFromSource(AudioSource source)`

```csharp
public void CopyFromSource(AudioSource source)
```

Copies all supported audio-related properties from the given `AudioSource`
into the internal [`AudioSourceData`][AudioSourceData] instance.

* Logs an error if `source` is null

### `ApplyToSource(AudioSource source)`

```csharp
public void ApplyToSource(AudioSource source)
```

Applies the stored audio configuration to the given `AudioSource`.

* Logs an error if `source` is null
* Overwrites all relevant `AudioSource` parameters

## Usage Examples

### Creating a Tone Asset

Create a new asset via the Unity menu:

```text
Assets → Create → Audipool → AudioPreset
```

### Capturing Settings from an AudioSource

```csharp
AudioPreset.CopyFromSource(audioSource);
```

### Playing Audio with a AudioPreset

```csharp
Audipool.Get()
    .WithAsset(AudioPreset)
    .Play();
```
[AudioSourceData]: ./audio-source-data

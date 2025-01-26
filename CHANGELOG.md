# Version 1.5.0
- Renamed the entire package to Audipool
  - Aftertone -> Audipool
  - ToneHandle -> AudioHandle
  - ToneAsset -> AudioPreset

- Added AudioSourceData class to hold AudioSource settings
- Added new methods to AudioHandle
  - SetPitch
  - SetVolume
  - SetPosition
  - SetResource
  - ApplySourceData
  - ApplyPreset

- Added **Max Alive Time** to AudipoolConfig

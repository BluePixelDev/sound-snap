import { defineConfig } from "vitepress";

// https://vitepress.dev/reference/site-config
export default defineConfig({
  srcDir: "content",
  base: "/aftertone/",
  head: [["link", { rel: "icon", href: "/base/favicon.png" }]],

  title: "Aftertone",
  description: "Aftertone unity plugin documentation website",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: "Home", link: "/" },
      { text: "Docs", link: "/docs" },
    ],

    sidebar: {
      "/docs": [
        {
          text: "Docs",
          items: [
            { text: "Installation", link: "/docs/installation" },
            { text: "Runtime API Examples", link: "/api-examples" },
          ],
        },
      ],
    },

    socialLinks: [
      {
        icon: "github",
        link: "https://github.com/BluePixelDev/unity-aftertone",
      },
    ],

    editLink: {
      pattern:
        "https://github.com/BluePixelDev/unity-aftertone/tree/pages/content/:path",
      text: "Edit this page on GitHub",
    },

    footer: { copyright: "Copyright © 2026-present Ondřej Kačírek" },
  },
});
